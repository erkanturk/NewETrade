using ETICARET.Business.Abstract;
using ETICARET.Business.Models;
using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Business.Concrete
{
    public class OrderManager(IOrderDal orderDal, ICartDal cartDal, IPaymentService paymentService) : IOrderService
    {
        public Task<int> CountAsync()
        => orderDal.CountAsync();

        public Task<List<Order>> GetAllOrdersAsync()
        => orderDal.GetAllWithItemsAsync();

        public Task<List<Order>> GetLatestOrdersAsync(int count = 5)
       => orderDal.GetLatestAsync(count);

        public Task<Order?> GetOrderDetailsAsync(int id)
       =>orderDal.GetDetailsAsync(id);

        public Task<decimal> GetTotalRevenueAsync()
       => orderDal.GetTotalRevenueAsync();

        public Task<List<Order>> GetUserOrdersAsync(string userId)
       =>orderDal.GetByUserAsync(userId);

        public async Task<ServiceResult> PlaceOrderAsync(CheckoutRequest request)
        {
            var cart =await cartDal.GetByUserIdAsync(request.UserId);
            if(cart is null|| cart.Items.Count==0) return ServiceResult.Fail("Sepetiniz boş.");
            foreach (var item in cart.Items)
            {
                if(item.Product is null || item.Product.Stock<item.Quantity)
                    return ServiceResult.Fail($"Ürün stokta yok: {item.Product?.Name}");
            }
            var order = new Order
            {
                OrderNumber=CreateOrderNumber(),
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                City = request.City,
                Address = request.Address,
                Note = request.Note,
                PaymentType = request.PaymentType,
                State=request.PaymentType==PaymentType.CreditCart?OrderState.Preparing:OrderState.PaymentPending,
                Items= [..cart.Items.Select(i=>new OrderItem{
                    ProductId=i.ProductId,
                    ProductName=i.Product!.Name,
                    Price=i.Product.Price,
                    Quantity=i.Quantity
                })]
            };
            if (request.PaymentType==PaymentType.CreditCart)
            {
                if(request.Card is null)
                    return ServiceResult.Fail("Kredi kartı bilgileri eksik.");
                var payment = await paymentService.PayAsync(order, request.Card, request.IpAddress);
                if(!payment.Success)
                    return ServiceResult.Fail($"Ödeme başarısız: {payment.ErrorMessage}");

                order.PaymentId=payment.PaymentId;
                order.ConversationId=payment.ConversationId;
            }

            foreach (var item in cart.Items)
            {
                item.Product!.Stock-=item.Quantity;
            }
            await orderDal.AddAsync(order);
            await cartDal.ClearAsync(cart.Id);
            return ServiceResult.Ok("Siparişiniz başarıyla oluşturuldu. Sipariş Numaranız:"+order.OrderNumber);
        }


        public async Task<ServiceResult> UpdateStateAsync(int orderId, OrderState state)
        {
            var order = await orderDal.GetByIdAsync(orderId);
            if(order is null) return ServiceResult.Fail("Sipariş bulunamadı.");

            order.State=state;
            await orderDal.UpdateAsync(order);
            return ServiceResult.Ok($"{order.OrderNumber}Sipariş durumu güncellendi.");
        }
        //ET261013130950476
        private static string CreateOrderNumber()
        {
            return $"ET{DateTime.Now:yyMMddHHmmss}{Random.Shared.Next(100,1000)}";
        }
    }
}
