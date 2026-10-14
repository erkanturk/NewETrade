using ETICARET.Entities;
using ETICARET.Entities.Dtos;

namespace ETICARET.DataAccess.Abstract;

public interface ICategoryDal : IRepository<Category>
{
    Task<List<CategoryWithCount>> GetAllWithCountAsync();
    Task<Category?> GetWithProductsAsync(int id);
}
