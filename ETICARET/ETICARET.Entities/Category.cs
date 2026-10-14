using System.ComponentModel.DataAnnotations;

namespace ETICARET.Entities;

public class Category : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }

    // Çoka-çok ilişkinin diğer ucu (Product.Categories ile eşleşir).
    public List<Product> Products { get; set; } = [];
}
