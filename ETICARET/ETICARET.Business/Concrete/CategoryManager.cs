using ETICARET.Business.Abstract;
using ETICARET.Business.Models;
using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using ETICARET.Entities.Dtos;

namespace ETICARET.Business.Concrete;

public class CategoryManager(ICategoryDal categoryDal) : ICategoryService
{
    public async Task<List<Category>> GetAllAsync()
        => [.. (await categoryDal.GetAllAsync()).OrderBy(c => c.Id)];

    public Task<List<CategoryWithCount>> GetAllWithCountAsync() => categoryDal.GetAllWithCountAsync();

    public Task<Category?> GetByIdAsync(int id) => categoryDal.GetByIdAsync(id);

    public Task<Category?> GetWithProductsAsync(int id) => categoryDal.GetWithProductsAsync(id);

    public Task<int> CountAsync() => categoryDal.CountAsync();

    public async Task<ServiceResult> CreateAsync(Category category)
    {
        category.Name = category.Name.Trim();

        // İş kuralı: Aynı isimde iki kategori olamaz.
        if (await categoryDal.AnyAsync(c => c.Name == category.Name))
            return ServiceResult.Fail($"\"{category.Name}\" adında bir kategori zaten var.");

        await categoryDal.AddAsync(category);
        return ServiceResult.Ok($"\"{category.Name}\" kategorisi eklendi.");
    }

    public async Task<ServiceResult> UpdateAsync(Category category)
    {
        category.Name = category.Name.Trim();

        if (await categoryDal.AnyAsync(c => c.Name == category.Name && c.Id != category.Id))
            return ServiceResult.Fail($"\"{category.Name}\" adında başka bir kategori var.");

        await categoryDal.UpdateAsync(category);
        return ServiceResult.Ok("Kategori güncellendi.");
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await categoryDal.GetByIdAsync(id);
        if (category is null)
            return ServiceResult.Fail("Kategori bulunamadı.");

        
        await categoryDal.DeleteAsync(category);
        return ServiceResult.Ok($"\"{category.Name}\" kategorisi silindi.");
    }
}
