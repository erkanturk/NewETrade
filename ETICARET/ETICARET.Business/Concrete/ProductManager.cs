using ETICARET.Business.Abstract;
using ETICARET.Business.Models;
using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using ETICARET.Entities.Dtos;

namespace ETICARET.Business.Concrete;


public class ProductManager(IProductDal productDal, ICategoryDal categoryDal) : IProductService
{
    public Task<List<Product>> GetHomeProductsAsync(int count = 10) => productDal.GetHomeProductsAsync(count);

    public Task<List<Product>> GetLatestAsync(int count = 10) => productDal.GetLatestAsync(count);


    public Task<PagedResult<Product>> GetProductsAsync(ProductFilter filter)
        => productDal.GetPagedAsync(filter with { Page = Math.Max(1, filter.Page) });

    public Task<Product?> GetDetailsAsync(int id) => productDal.GetDetailsAsync(id);

    public Task<List<Product>> GetRelatedAsync(int productId, int count = 5) => productDal.GetRelatedAsync(productId, count);

    public Task<List<Product>> GetAllForAdminAsync() => productDal.GetAllForAdminAsync();

    public Task<List<Product>> GetLowStockAsync(int threshold = 5) => productDal.GetLowStockAsync(threshold);

    public Task<int> CountAsync() => productDal.CountAsync();

    public async Task<ServiceResult> CreateAsync(Product product, int[] categoryIds)
    {
        // İş kuralı: Her ürün en az bir kategoride olmalı.
        if (categoryIds.Length == 0)
            return ServiceResult.Fail("En az bir kategori seçmelisiniz.");

        product.Categories = await categoryDal.GetAllAsync(c => categoryIds.Contains(c.Id));
        await productDal.AddAsync(product);

        return ServiceResult.Ok($"\"{product.Name}\" eklendi.");
    }

    /// <param name="product">GetDetailsAsync ile okunup değiştirilmiş (EF tarafından takip edilen) ürün.</param>
    public async Task<ServiceResult> UpdateAsync(Product product, int[] categoryIds)
    {
        if (categoryIds.Length == 0)
            return ServiceResult.Fail("En az bir kategori seçmelisiniz.");

        // Seçimi kaldırılan kategorileri çıkar, yeni seçilenleri ekle.
        // Ara tablodaki (ProductCategories) satırları EF Core kendisi siler/ekler.
        product.Categories.RemoveAll(c => !categoryIds.Contains(c.Id));

        var newIds = categoryIds.Where(id => product.Categories.All(c => c.Id != id)).ToArray();
        if (newIds.Length > 0)
            product.Categories.AddRange(await categoryDal.GetAllAsync(c => newIds.Contains(c.Id)));

        await productDal.UpdateAsync(product);
        return ServiceResult.Ok($"\"{product.Name}\" güncellendi.");
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productDal.GetByIdAsync(id);
        if (product is null)
            return ServiceResult.Fail("Ürün bulunamadı.");

        // İş kuralı: Sipariş edilmiş ürün silinmez, geçmiş siparişler bozulmasın.
        if (await productDal.HasOrdersAsync(id))
            return ServiceResult.Fail("Bu ürün siparişlerde yer aldığı için silinemez. Satışı durdurmak için stoğunu 0 yapabilirsiniz.");

        await productDal.DeleteAsync(product);
        return ServiceResult.Ok($"\"{product.Name}\" silindi.");
    }
}
