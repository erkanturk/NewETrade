using ETICARET.Business.Abstract;
using ETICARET.Business.Models;
using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;

namespace ETICARET.Business.Concrete;

public class OrderManager(IOrderDal orderDal, ICartDal cartDal, IPaymentService paymentService) : IOrderService
{
    /// <summary>
    /// Sipariş verme akışı (iş kurallarının toplandığı yer):
    /// 1) Sepet boş mu?  2) Stok yeterli mi?  3) Kartla ödemeyse ödemeyi al
    /// 4) Siparişi kaydet + stok düş  5) Sepeti boşalt
    /// </summary>
    public async Task<ServiceResult> PlaceOrderAsync(CheckoutRequest request)
    {
        var cart = await cartDal.GetByUserIdAsync(request.UserId);
        if (cart is null || cart.Items.Count == 0)
            return ServiceResult.Fail("Sepetiniz boş.");

        foreach (var item in cart.Items)
        {
            if (item.Product is null || item.Product.Stock < item.Quantity)
                return ServiceResult.Fail($"\"{item.Product?.Name}\" için yeterli stok kalmadı. Lütfen sepetinizi güncelleyin.");
        }

        var order = new Order
        {
            OrderNumber = CreateOrderNumber(),
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            City = request.City,
            Address = request.Address,
            Note = request.Note,
            PaymentType = request.PaymentType,
            // Kartla ödenen sipariş direkt hazırlanır, havale/EFT'de ödeme beklenir.
            State = request.PaymentType == PaymentType.CreditCard ? OrderState.Preparing : OrderState.PaymentPending,
            Items = [.. cart.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.Product!.Name,
                Price = i.Product.Price,
                Quantity = i.Quantity
            })]
        };

        if (request.PaymentType == PaymentType.CreditCard)
        {
            if (request.Card is null)
                return ServiceResult.Fail("Kart bilgileri eksik.");

            var payment = await paymentService.PayAsync(order, request.Card, request.IpAddress);
            if (!payment.Success)
                return ServiceResult.Fail($"Ödeme alınamadı: {payment.ErrorMessage}");

            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
        }

        // Stok düşülür. Ürünler sepet sorgusuyla (aynı DbContext üzerinden) yüklendiği için
        // EF bu değişikliği takip ediyor; aşağıdaki AddAsync içindeki tek SaveChanges
        // hem siparişi hem de yeni stokları birlikte kaydeder.
        foreach (var item in cart.Items)
            item.Product!.Stock -= item.Quantity;

        await orderDal.AddAsync(order);
        await cartDal.ClearAsync(cart.Id);

        return ServiceResult.Ok($"Siparişiniz alındı. Sipariş numaranız: {order.OrderNumber}");
    }

    public Task<List<Order>> GetUserOrdersAsync(string userId) => orderDal.GetByUserAsync(userId);

    public Task<List<Order>> GetAllOrdersAsync() => orderDal.GetAllWithItemsAsync();

    public Task<List<Order>> GetLatestOrdersAsync(int count = 5) => orderDal.GetLatestAsync(count);

    public Task<Order?> GetOrderDetailsAsync(int id) => orderDal.GetDetailsAsync(id);

    public Task<int> CountAsync() => orderDal.CountAsync();

    public Task<decimal> GetTotalRevenueAsync() => orderDal.GetTotalRevenueAsync();

    public async Task<ServiceResult> UpdateStateAsync(int orderId, OrderState state)
    {
        var order = await orderDal.GetByIdAsync(orderId);
        if (order is null)
            return ServiceResult.Fail("Sipariş bulunamadı.");

        order.State = state;
        await orderDal.UpdateAsync(order);
        return ServiceResult.Ok($"{order.OrderNumber} numaralı siparişin durumu güncellendi.");
    }

    // Örnek: ET2610081530427  (ET + yıl ay gün saat dakika saniye + rastgele 3 hane)
    private static string CreateOrderNumber()
        => $"ET{DateTime.Now:yyMMddHHmmss}{Random.Shared.Next(100, 1000)}";
}
