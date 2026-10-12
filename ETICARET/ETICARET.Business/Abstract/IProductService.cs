using ETICARET.Business.Models;
using ETICARET.Entities;
using ETICARET.Entities.Dtos;

namespace ETICARET.Business.Abstract;

public interface IProductService
{
    Task<List<Product>> GetHomeProductsAsync(int count = 10);
    Task<List<Product>> GetLatestAsync(int count = 10);
    Task<PagedResult<Product>> GetProductsAsync(ProductFilter filter);
    Task<Product?> GetDetailsAsync(int id);
    Task<List<Product>> GetRelatedAsync(int productId, int count = 5);

    // Admin
    Task<List<Product>> GetAllForAdminAsync();
    Task<List<Product>> GetLowStockAsync(int threshold = 5);
    Task<int> CountAsync();
    Task<ServiceResult> CreateAsync(Product product, int[] categoryIds);
    Task<ServiceResult> UpdateAsync(Product product, int[] categoryIds);
    Task<ServiceResult> DeleteAsync(int id);
}
