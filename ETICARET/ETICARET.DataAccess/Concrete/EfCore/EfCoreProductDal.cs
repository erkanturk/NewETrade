using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using ETICARET.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.DataAccess.Concrete.EfCore;

public class EfCoreProductDal(EticaretContext context)
    : EfCoreGenericRepository<Product>(context), IProductDal
{
    // Liste ekranlarında sadece ilk resim gerekir.
    // NOT (Filtered Include): Include içinde OrderBy/Take yazarak ilişkili kayıtları süzebiliriz.
    // NOT (AsNoTracking): Sadece okunup ekrana basılacak verilerde EF değişiklik takibi yapmaz → daha hızlı.
    private IQueryable<Product> ListQuery()
        => Table.AsNoTracking().Include(p => p.Images.OrderBy(i => i.Id).Take(1));

    public async Task<List<Product>> GetHomeProductsAsync(int count)
        => await ListQuery()
            .Where(p => p.IsHome && p.Stock > 0)
            .OrderBy(p => p.Id)
            .Take(count)
            .ToListAsync();

    public async Task<List<Product>> GetLatestAsync(int count)
        => await ListQuery()
            .OrderByDescending(p => p.Id)
            .Take(count)
            .ToListAsync();

    public async Task<PagedResult<Product>> GetPagedAsync(ProductFilter filter)
    {
        // IQueryable: sorgu henüz çalışmadı, parça parça inşa ediliyor.
        var query = ListQuery();

        if (!string.IsNullOrWhiteSpace(filter.Category))
            query = query.Where(p => p.Categories.Any(c => c.Name == filter.Category));

        if (!string.IsNullOrWhiteSpace(filter.Search))
            query = query.Where(p => p.Name.Contains(filter.Search)
                                  || (p.Description != null && p.Description.Contains(filter.Search)));

        // switch expression (C# 8): seçilen sıralamaya göre ORDER BY ekle
        query = filter.Sort switch
        {
            ProductSort.PriceAsc => query.OrderBy(p => p.Price),
            ProductSort.PriceDesc => query.OrderByDescending(p => p.Price),
            ProductSort.Newest => query.OrderByDescending(p => p.Id),
            _ => query.OrderByDescending(p => p.Stock > 0).ThenByDescending(p => p.IsHome).ThenBy(p => p.Id)
        };

        var totalCount = await query.CountAsync();

        // Sayfalama: Skip = atla, Take = al. Sorgu ancak ToListAsync ile veritabanına gider.
        var items = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedResult<Product>(items, totalCount);
    }

    // Detay ve düzenleme ekranı için: bütün resimler + kategoriler.
    // Takip EDİLEN (tracking) sorgu; admin bu nesneyi değiştirip kaydedebilir.
    // NOT (AsSplitQuery): İki koleksiyon Include edildiğinde tek büyük sorgu yerine ayrı sorgular atılır.
    public async Task<Product?> GetDetailsAsync(int id)
        => await Table
            .Include(p => p.Images)
            .Include(p => p.Categories)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Product>> GetRelatedAsync(int productId, int count)
    {
        // Aynı kategorideki diğer ürünler
        var categoryIds = Context.Products
            .Where(p => p.Id == productId)
            .SelectMany(p => p.Categories.Select(c => c.Id));

        return await ListQuery()
            .Where(p => p.Id != productId && p.Stock > 0
                     && p.Categories.Any(c => categoryIds.Contains(c.Id)))
            .OrderByDescending(p => p.IsHome)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllForAdminAsync()
        => await ListQuery()
            .Include(p => p.Categories)
            .AsSplitQuery()
            .OrderByDescending(p => p.Id)
            .ToListAsync();

    public async Task<List<Product>> GetLowStockAsync(int threshold)
        => await Table.AsNoTracking()
            .Where(p => p.Stock <= threshold)
            .OrderBy(p => p.Stock)
            .ToListAsync();

    public async Task<bool> HasOrdersAsync(int productId)
        => await Context.OrderItems.AnyAsync(i => i.ProductId == productId);
}
