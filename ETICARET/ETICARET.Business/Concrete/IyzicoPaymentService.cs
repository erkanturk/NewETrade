using System.Globalization;
using ETICARET.Business.Abstract;
using ETICARET.Business.Models;
using ETICARET.Business.Settings;
using ETICARET.Entities;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
// İki farklı kütüphanede "Options" adında tip var; karışmasın diye takma ad (alias) veriyoruz.
using IyzicoOptions = Iyzipay.Options;

namespace ETICARET.Business.Concrete;

/// <summary>
/// Iyzico sanal POS ile kredi kartından ödeme alır.
/// Anahtarlar appsettings.json → "Iyzico" bölümünden gelir (Options pattern).
/// Sandbox test kartı: 5528 7900 0000 0008 · 12/2030 · 123
/// </summary>
public class IyzicoPaymentService(IOptions<IyzicoSettings> options, ILogger<IyzicoPaymentService> logger)
    : IPaymentService
{
    public async Task<PaymentResult> PayAsync(Order order, CardInfo card, string ipAddress)
    {
        var settings = options.Value;
        var iyzicoOptions = new IyzicoOptions
        {
            BaseUrl = settings.BaseUrl,
            ApiKey = settings.ApiKey,
            SecretKey = settings.SecretKey
        };

        var address = new Address
        {
            ContactName = order.FullName,
            City = order.City,
            Country = "Turkey",
            Description = order.Address
        };

        var request = new CreatePaymentRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = Guid.NewGuid().ToString(),
            Price = ToIyzicoPrice(order.TotalPrice),
            PaidPrice = ToIyzicoPrice(order.TotalPrice),
            Currency = Currency.TRY.ToString(),
            Installment = 1,
            BasketId = order.OrderNumber,
            PaymentChannel = PaymentChannel.WEB.ToString(),
            PaymentGroup = PaymentGroup.PRODUCT.ToString(),
            PaymentCard = new PaymentCard
            {
                CardHolderName = card.HolderName,
                CardNumber = card.Number.Replace(" ", ""),
                ExpireMonth = card.ExpireMonth,
                ExpireYear = card.ExpireYear,
                Cvc = card.Cvc,
                RegisterCard = 0
            },
            Buyer = new Buyer
            {
                Id = order.UserId,
                Name = order.FirstName,
                Surname = order.LastName,
                Email = order.Email,
                GsmNumber = order.Phone,
                IdentityNumber = "11111111111", // Gerçek projede kullanıcıdan alınır
                RegistrationAddress = order.Address,
                City = order.City,
                Country = "Turkey",
                Ip = ipAddress
            },
            ShippingAddress = address,
            BillingAddress = address,
            // Sepet kalemlerinin toplamı, Price ile birebir aynı olmalıdır (Iyzico kuralı).
            BasketItems = [.. order.Items.Select(i => new BasketItem
            {
                Id = i.ProductId.ToString(),
                Name = i.ProductName,
                Category1 = "Genel",
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Price = ToIyzicoPrice(i.Price * i.Quantity)
            })]
        };

        try
        {
            var payment = await Payment.Create(request, iyzicoOptions);

            return payment.Status == Status.SUCCESS.ToString()
                ? new PaymentResult(true, payment.PaymentId, payment.ConversationId, null)
                : PaymentResult.Fail(payment.ErrorMessage ?? "Bilinmeyen hata");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Iyzico ödeme isteği başarısız. Sipariş: {OrderNumber}", order.OrderNumber);
            return PaymentResult.Fail("Ödeme servisine ulaşılamadı. Lütfen daha sonra tekrar deneyin.");
        }
    }

    // Iyzico fiyatı nokta ile ayrılmış metin olarak ister: 1234.50
    private static string ToIyzicoPrice(decimal price) => price.ToString("0.00", CultureInfo.InvariantCulture);
}
