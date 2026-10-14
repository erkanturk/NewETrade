using System.ComponentModel.DataAnnotations;

namespace ETICARET.Entities;

/// <summary>Her kullanıcının tek bir sepeti vardır.</summary>
public class Cart : BaseEntity
{
    [MaxLength(450)]
    public required string UserId { get; set; }

    public List<CartItem> Items { get; set; } = [];

    // Sadece "get" olan (hesaplanan) özellikler veritabanına kolon olarak YAZILMAZ.
    // NOT: "=>" ile tanımlanan özellik her okunduğunda yeniden hesaplanır.
    public decimal TotalPrice => Items.Sum(i => (i.Product?.Price ?? 0) * i.Quantity);

    public int TotalQuantity => Items.Sum(i => i.Quantity);
}
