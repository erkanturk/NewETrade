using ETICARET.Entities;
using ETICARET.Entities.Dtos;

namespace ETICARET.WebUI.Models;

// Sadece ekranda gösterilecek (okunacak) veriler için ViewModel'ler.
// Ürün kartlarında Product entity'sini doğrudan kullanmak küçük projelerde kabul edilebilir;
// FORMLARDA ise mutlaka ayrı model kullanılır (bkz. CheckoutModel, ProductFormModel).

public record HomeViewModel(
    List<CategoryWithCount> Categories,
    List<Product> FeaturedProducts,
    List<Product> LatestProducts);

public class ProductListViewModel
{
    public required List<Product> Products { get; init; }
    public required PageInfo PageInfo { get; init; }
    public string? Category { get; init; }
    public string? Search { get; init; }
    public ProductSort Sort { get; init; }

    // Pattern matching: (kategori, arama) ikilisine göre sayfa başlığı
    public string Title => (Category, Search) switch
    {
        (_, { Length: > 0 } search) => $"\"{search}\" için sonuçlar",
        ({ Length: > 0 } category, _) => category,
        _ => "Tüm Ürünler"
    };
}

public record ProductDetailsViewModel(
    Product Product,
    List<Product> RelatedProducts,
    CommentListViewModel Comments);

public record CommentListViewModel(int ProductId, List<Comment> Comments);

public record CategoryMenuViewModel(List<CategoryWithCount> Categories, string? SelectedCategory);
