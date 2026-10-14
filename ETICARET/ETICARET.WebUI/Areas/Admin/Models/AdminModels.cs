using System.ComponentModel.DataAnnotations;
using ETICARET.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ETICARET.WebUI.Areas.Admin.Models;

public record DashboardViewModel(
    int ProductCount,
    int CategoryCount,
    int OrderCount,
    int UserCount,
    decimal Revenue,
    List<Order> LatestOrders,
    List<Product> LowStockProducts);

/// <summary>
/// Ürün ekleme/düzenleme formu. Entity yerine ayrı model kullanmamızın sebebi:
/// formdan sadece izin verdiğimiz alanlar gelsin (overposting saldırısına karşı)
/// ve dosya yükleme gibi entity'de olmayan alanlar taşınabilsin.
/// Kurallar: Validators/ProductFormModelValidator.cs (FluentValidation)
/// </summary>
public class ProductFormModel
{
    public int Id { get; set; }

    [Display(Name = "Ürün adı")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Display(Name = "Fiyat (₺)")]
    public decimal Price { get; set; }

    [Display(Name = "Stok adedi")]
    public int Stock { get; set; }

    [Display(Name = "Ana sayfada göster")]
    public bool IsHome { get; set; }

    [Display(Name = "Kategoriler")]
    public int[] CategoryIds { get; set; } = [];

    [Display(Name = "Resimler")]
    public List<IFormFile> Files { get; set; } = [];

    // Düzenlemede silinmesi işaretlenen mevcut resimler
    public int[] DeleteImageIds { get; set; } = [];

    // Sadece ekranda göstermek için (formdan gelmez)
    [BindNever, ValidateNever]
    public List<Image> ExistingImages { get; set; } = [];

    [BindNever, ValidateNever]
    public List<Category> AllCategories { get; set; } = [];
}

public class CategoryFormModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "2-100 karakter arasında olmalıdır.")]
    [Display(Name = "Kategori adı")]
    public string Name { get; set; } = string.Empty;

    [BindNever, ValidateNever]
    public List<Product> Products { get; set; } = [];
}

public record UserListItem(string Id, string UserName, string FullName, string Email, bool EmailConfirmed, bool IsAdmin);
