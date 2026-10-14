using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using ETICARET.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.DataAccess.Concrete.EfCore;

public class EfCoreCategoryDal(EticaretContext context)
    : EfCoreGenericRepository<Category>(context), ICategoryDal
{
    // Projeksiyon (Select): Bütün ürünleri belleğe çekmek yerine sadece
    // ihtiyacımız olan sayıyı ve ilk resmi SQL tarafında hesaplatıyoruz.
    public async Task<List<CategoryWithCount>> GetAllWithCountAsync()
        => await Table.AsNoTracking()
            .OrderBy(c => c.Id)
            .Select(c => new CategoryWithCount(
                c.Id,
                c.Name,
                c.Products.Count,
                c.Products.OrderByDescending(p => p.IsHome)
                    .SelectMany(p => p.Images.OrderBy(i => i.Id).Take(1))
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault()))
            .ToListAsync();

    public async Task<Category?> GetWithProductsAsync(int id)
        => await Table
            .Include(c => c.Products)
            .ThenInclude(p => p.Images.OrderBy(i => i.Id).Take(1))
            .FirstOrDefaultAsync(c => c.Id == id);
}
