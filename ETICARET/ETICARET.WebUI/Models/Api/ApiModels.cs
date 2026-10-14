namespace ETICARET.WebUI.Models.Api;

// API'den dışarı verilen veriler. Entity'leri doğrudan döndürmüyoruz:
// - İç yapı (Comments, Cart...) dışarı sızmaz
// - İlişkilerden doğan sonsuz döngü (Product → Category → Product...) olmaz

public record ProductListItemDto(int Id, string Name, decimal Price, int Stock, string ImageUrl);

public record ProductDetailDto(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    List<string> Images,
    List<string> Categories);

public record CategoryDto(int Id, string Name, int ProductCount);

public record PagedResponse<T>(List<T> Items, int Page, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
