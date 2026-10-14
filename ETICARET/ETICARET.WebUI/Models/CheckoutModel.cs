using System.ComponentModel.DataAnnotations;
using ETICARET.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ETICARET.WebUI.Models;

/// <summary>
/// Ödeme formu. Kuralları FluentValidation ile yazıldı (Validators/CheckoutModelValidator.cs)
/// çünkü "kart bilgileri sadece kredi kartı seçiliyse zorunlu" gibi koşullu kurallar var.
/// [Display] → label'larda görünen ad.
/// </summary>
public class CheckoutModel
{
    [Display(Name = "Ad")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Soyad")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Telefon")]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Şehir")]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Adres")]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "Sipariş notu")]
    public string? Note { get; set; }

    public PaymentType PaymentType { get; set; } = PaymentType.CreditCard;

    [Display(Name = "Kart üzerindeki isim")]
    public string? CardHolderName { get; set; }

    [Display(Name = "Kart numarası")]
    public string? CardNumber { get; set; }

    [Display(Name = "Ay")]
    public string? ExpireMonth { get; set; }

    [Display(Name = "Yıl")]
    public string? ExpireYear { get; set; }

    [Display(Name = "CVC")]
    public string? Cvc { get; set; }

    // Sağdaki sipariş özeti için. Formdan GELMEZ ([BindNever]) ve doğrulanmaz ([ValidateNever]).
    [BindNever, ValidateNever]
    public Cart? Cart { get; set; }
}
