using ETICARET.Business.Abstract;
using ETICARET.Business.Models;
using ETICARET.Business.Settings;
using ETICARET.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Iyzipay.Model;
using Iyzipay.Request;
using IyzicoOptions = Iyzipay.Options;

namespace ETICARET.Business.Concrete;

public class IyzicoPaymentService(IOptions<IyzicoSettings> options, ILogger<IyzicoPaymentService> logger) : IPaymentService
{
    public async Task<PaymentResult> PayAsync(Order order, CardInfo card, string ipAddress)
    {
       var settings = options.Value;
        var iyzicoOptions = new IyzicoOptions
        {
            BaseUrl = settings.BaseUrl,
            ApiKey = settings.ApiKey,
            SecretKey =settings.SecretKey
        };
        var address = new Address
        {
            ContactName=order.FullName,
            City=order.City,
            Country="Turkiye",
            Description=order.Address
        };
        var request = new CreatePaymentRequest
        {
            Locale=Locale.TR.ToString(),
            ConversationId=Guid.NewGuid().ToString(),
            Price=ToIyzicoPrice(order.TotalPrice),
            Currency=Currency.TRY.ToString(),
            Installment=1,
            BasketId=order.OrderNumber,
            PaymentChannel=PaymentChannel.WEB.ToString(),
            PaymentGroup=PaymentGroup.PRODUCT.ToString(),
            PaymentCard=new PaymentCard
            {
                CardHolderName=card.HolderName,
                CardNumber=card.Number.Replace(" ",""),
                ExpireMonth=card.ExpireMonth,
                ExpireYear=card.ExpireYear,
                Cvc=card.CvC,
                RegisterCard=0
            },
            Buyer=new Buyer
            {
                Id=order.UserId,
                Name=order.FirstName,
                Surname=order.LastName,
                GsmNumber=order.Phone,
                Email=order.Email,
                IdentityNumber="11111111111",//Gerçek projede kullanıcıdan alınacak
                RegistrationAddress=order.Address,
                Ip=ipAddress,
                City=order.City,
                Country="Turkiye",
                
            },
            ShippingAddress=address,
            BillingAddress=address,
            BasketItems= [..order.Items.Select(i=>new BasketItem{
                Id=i.ProductId.ToString(),
                Name=i.ProductName,
                Category1="Genel",
                ItemType=BasketItemType.PHYSICAL.ToString(),
                Price=ToIyzicoPrice(i.Price*i.Quantity)
            })]
        };
        try
        {
            var payment = await Payment.Create(request, iyzicoOptions);
            return payment.Status==Status.SUCCESS.ToString()?
                new PaymentResult(true,payment.PaymentId,payment.ConversationId,null):
                PaymentResult.Fail(payment.ErrorMessage??"Bilinmeyen hata");
        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Iyzico ödeme isteği başarısız. Sipariş:{OrderNumber}", order.OrderNumber);
            return PaymentResult.Fail("Ödeme servisine ulaşılamadı lütfen sonra tekrar deneyin");
        }
    }
    //1234.50
    private static string ToIyzicoPrice(decimal price) => price.ToString("0.00", CultureInfo.InvariantCulture);
}

