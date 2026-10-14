using System.ComponentModel.DataAnnotations;

namespace ETICARET.Entities;

/// <summary>Ürün resmi. Dosyanın kendisi wwwroot/img/products klasöründe, burada sadece adı tutulur.</summary>
public class Image : BaseEntity
{
    [MaxLength(300)]
    public required string ImageUrl { get; set; }

    // Bire-çok ilişki: ProductId → yabancı anahtar (foreign key), Product → navigation property
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
