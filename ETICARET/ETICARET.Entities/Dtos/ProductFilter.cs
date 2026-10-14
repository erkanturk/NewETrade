namespace ETICARET.Entities.Dtos;

/// <summary>
/// Ürün listeleme sayfasının filtreleri.
/// NOT (C# 9): "record" → sadece veri taşıyan, değiştirilemeyen (immutable) küçük tip.
/// Parantez içindeki parametreler otomatik olarak özelliğe dönüşür (positional record).
/// </summary>
/// 
/// <summary>
/// Record keywordu sadece veriyi yaşıyan değiştirilemeyen (immutable) nesneler oluşturmak için kullanılır.
/// Recordlar, sınıflara benzer şekilde davranır, ancak değer tabanlı eşitlik ve kopyalama gibi 
/// özelliklere sahiptir. Recordlar, genellikle veri taşıma nesneleri (DTO) veya immutable veri yapıları için tercih edilir.
/// </summary>
public record ProductFilter(
    string? Category = null,
    string? Search = null,
    ProductSort Sort = ProductSort.Recommended,
    int Page = 1,
    int PageSize = 12);

public enum ProductSort
{
    Recommended,
    PriceAsc,
    PriceDesc,
    Newest
}
