using System.ComponentModel.DataAnnotations;

namespace ETICARET.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    // Ürün adı ve fiyatı SİPARİŞ ANINDAKİ haliyle saklanır.
    // Ürünün fiyatı sonradan değişse de geçmiş siparişin tutarı değişmez.
    [MaxLength(200)]
    public required string ProductName { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
