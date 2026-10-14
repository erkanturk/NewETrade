using System.ComponentModel.DataAnnotations;

namespace ETICARET.Entities;

public class Product : BaseEntity
{
    // NOT (C# 11): "required" → nesne oluşturulurken bu alan mutlaka verilmeli.
    [MaxLength(200)]
    public required string Name { get; set; }

    // "string?" → boş (NULL) olabilir demek. Veritabanında da NULL kabul eden kolon olur.
    [MaxLength(4000)]
    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    // Ana sayfada "Öne çıkanlar" bölümünde gösterilsin mi?
    public bool IsHome { get; set; }

    // NOT (C# 12): [] → collection expression, "new List<Image>()" ile aynı şey.
    public List<Image> Images { get; set; } = [];

    // Çoka-çok ilişki: bir ürün birden çok kategoride olabilir.
    // EF Core ara tabloyu (ProductCategories) kendisi oluşturur.
    public List<Category> Categories { get; set; } = [];

    public List<Comment> Comments { get; set; } = [];
}
