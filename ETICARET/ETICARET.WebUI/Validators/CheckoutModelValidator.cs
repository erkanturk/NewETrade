using ETICARET.Entities;
using ETICARET.WebUI.Models;
using FluentValidation;

namespace ETICARET.WebUI.Validators;

/// <summary>
/// FluentValidation 12: Kurallar model sınıfından ayrı, okunabilir zincirler halinde yazılır.
/// Data Annotations ile zor olan KOŞULLU kural burada kolay: When(...)
/// Program.cs'teki AddValidatorsFromAssemblyContaining bu sınıfı otomatik bulup DI'a kaydeder.
/// </summary>
public class CheckoutModelValidator : AbstractValidator<CheckoutModel>
{
    public CheckoutModelValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad zorunludur.").MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad zorunludur.").MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir e-posta girin.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefon zorunludur.")
            .Matches(@"^[0-9 +()-]{10,20}$").WithMessage("Geçerli bir telefon numarası girin.");

        RuleFor(x => x.City).NotEmpty().WithMessage("Şehir zorunludur.").MaximumLength(50);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres zorunludur.")
            .MinimumLength(10).WithMessage("Adres en az 10 karakter olmalıdır.")
            .MaximumLength(500);

        RuleFor(x => x.Note).MaximumLength(500);

        RuleFor(x => x.PaymentType).IsInEnum();

        // Kart bilgileri SADECE kredi kartı seçildiyse zorunlu
        When(x => x.PaymentType == PaymentType.CreditCard, () =>
        {
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Kart üzerindeki isim zorunludur.");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Kart numarası zorunludur.")
                .CreditCard().WithMessage("Kart numarası geçersiz.");   // Luhn algoritması ile kontrol

            RuleFor(x => x.ExpireMonth).NotEmpty().WithMessage("Ay seçin.");
            RuleFor(x => x.ExpireYear).NotEmpty().WithMessage("Yıl seçin.");

            RuleFor(x => x.Cvc)
                .NotEmpty().WithMessage("CVC zorunludur.")
                .Matches(@"^\d{3,4}$").WithMessage("CVC 3 veya 4 haneli olmalıdır.");
        });
    }
}
