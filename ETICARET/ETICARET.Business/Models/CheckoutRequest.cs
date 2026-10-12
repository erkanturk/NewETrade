using ETICARET.Entities;
using Iyzipay.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Business.Models;

/// <summary>
/// Controller formdan gelen bilgiyi burayaki yapıya çevirip ORderManage'a veririz.
/// Init değer sadece nesne oluşturulurken atanabilir ve sonrasında değiştirilemez. 
/// Bu, nesnenin oluşturulma anında belirli özelliklerin sabit kalmasını sağlar.
/// </summary>
public class CheckoutRequest
{
    public required string UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string City { get; init; }
    public required string Address { get; init; }
    public string? Note { get; init; }
    public PaymentType PaymentType { get; init; }
    public CardInfo? Card { get; init; }//Sadece kredi kartı ile ödeme yapılacaksa dolu olacak. Diğer ödeme tiplerinde null olacak.
    public string IpAddress { get; init; } = "127.0.0.1";

}
public record CardInfo(string HolderName, string Number, string ExpireMonth, string ExpireYear, string CvC);
