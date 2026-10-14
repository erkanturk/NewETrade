using ETICARET.WebUI.Areas.Admin.Models;
using FluentValidation;

namespace ETICARET.WebUI.Validators;

public class ProductFormModelValidator : AbstractValidator<ProductFormModel>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

    public ProductFormModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adı zorunludur.")
            .Length(3, 200).WithMessage("Ürün adı 3-200 karakter olmalıdır.");

        RuleFor(x => x.Description).MaximumLength(4000);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.")
            .LessThan(10_000_000).WithMessage("Fiyat çok yüksek.");

        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stok negatif olamaz.");

        RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("En az bir kategori seçin.");

        // Yeni ürün (Id == 0) için en az bir resim zorunlu
        RuleFor(x => x.Files)
            .NotEmpty().When(x => x.Id == 0)
            .WithMessage("En az bir ürün resmi yükleyin.");

        // Her dosya için ayrı kural: uzantı ve boyut
        RuleForEach(x => x.Files)
            .Must(f => AllowedExtensions.Contains(Path.GetExtension(f.FileName).ToLowerInvariant()))
            .WithMessage("Sadece jpg, png veya webp yükleyebilirsiniz.")
            .Must(f => f.Length <= MaxFileSize)
            .WithMessage("Bir resim en fazla 5 MB olabilir.");
    }
}
