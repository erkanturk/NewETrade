using ETICARET.Business.Models;
using ETICARET.Entities;
using ETICARET.Entities.Dtos;

namespace ETICARET.Business.Abstract;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync();
    Task<List<CategoryWithCount>> GetAllWithCountAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> GetWithProductsAsync(int id);
    Task<int> CountAsync();
    Task<ServiceResult> CreateAsync(Category category);
    Task<ServiceResult> UpdateAsync(Category category);
    Task<ServiceResult> DeleteAsync(int id);
}
