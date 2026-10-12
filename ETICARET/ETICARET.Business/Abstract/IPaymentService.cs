using ETICARET.Business.Models;
using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Business.Abstract
{
    public interface IPaymentService
    {
        Task<PaymentResult> PayAsync(Order order, CardInfo card, string ipAddress);
    }
}
