using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Entities.Dtos;
/// <summary>
/// Ürün listeleme sayfasının filtreleri
/// Record sadece veri taşıyan değiştirilemeyen (immutable) küçük tip.
/// PArantez içindeki parametreler otomatik olarak özelliğe dönüşür.
/// </summary>

public record ProductFilter(
    string? Category = null,
    string? Search = null,
    int Page = 1,
    int PageSize = 12
    );
public enum ProductSort
{
    Recommended,
    PriceAsc,
    PriceDesc,
    Newest
}