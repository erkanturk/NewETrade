using ETICARET.Business.Models;
using ETICARET.Entities;

namespace ETICARET.Business.Abstract;

/// <summary>
/// Ödeme servisi. Bugün Iyzico, yarın başka bir firma olabilir;
/// OrderManager sadece bu arayüzü bilir (Dependency Inversion).
/// </summary>
public interface IPaymentService
{
    Task<PaymentResult> PayAsync(Order order, CardInfo card, string ipAddress);
}
