using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using ETICARET.Entities.Dtos;
using Microsoft.EntityFrameworkCore;


namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal(EticaretContext context)
    : EfCoreGenericRepository<Product>(context), IProductDal
    {
        private IQueryable<Product> ListQuery() => Table.AsNoTracking().Include(p => p.Images.OrderBy(i => i.Id).Take(1));
        //AsSplitQuery Sorgudan gelen değerler parçalayarak getir tüm değerleri getirme
        public async Task<List<Product>> GetAllForAdminAsync()
       => await ListQuery().Include(p => p.Categories).AsSplitQuery().OrderByDescending(p => p.Id).ToListAsync();

        public async Task<Product?> GetDetailsAsync(int id)
      => await Table.Include(p => p.Images)
            .Include(p => p.Categories)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id==id);


        public async Task<List<Product>> GetHomeProductsAsync(int count)
        => await ListQuery().Where(p => p.IsHome&&p.Stock>0).OrderBy(p => p.Id).Take(count).ToListAsync();

        public async Task<List<Product>> GetLatestAsync(int count)
       => await ListQuery().OrderByDescending(p => p.Id).Take(count).ToListAsync();

        public Task<List<Product>> GetLowStockAsync(int threshold)
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<Product>> GetPagedAsync(ProductFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetRelatedAsync(int productId, int count)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasOrdersAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
