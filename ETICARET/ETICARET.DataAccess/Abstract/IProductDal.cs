using ETICARET.Entities;
using ETICARET.Entities.Dtos;

namespace ETICARET.DataAccess.Abstract;

// IRepository<Product>'taki ortak metotlara ek olarak ürüne özel sorgular.
public interface IProductDal : IRepository<Product>
{
    Task<List<Product>> GetHomeProductsAsync(int count);
    Task<List<Product>> GetLatestAsync(int count);
    Task<PagedResult<Product>> GetPagedAsync(ProductFilter filter);
    Task<Product?> GetDetailsAsync(int id);
    Task<List<Product>> GetRelatedAsync(int productId, int count);
    Task<List<Product>> GetAllForAdminAsync();
    Task<List<Product>> GetLowStockAsync(int threshold);
    Task<bool> HasOrdersAsync(int productId);
}
