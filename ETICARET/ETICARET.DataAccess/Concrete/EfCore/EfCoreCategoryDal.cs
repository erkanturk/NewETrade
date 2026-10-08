using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using ETICARET.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal(EticaretContext context) : EfCoreGenericRepository<Category>(context), ICategoryDal
    {
        public async Task<List<CategoryWithCount>> GetAllWithCountAsync()
       => await Table.AsNoTracking().OrderBy(c=>c.Id).Select(c=>new CategoryWithCount(
           c.Id,c.Name,c.Products.Count,c.Products.OrderByDescending(p=>p.IsHome).SelectMany(p=>p.Images.OrderBy(i=>i.Id).Take(1)).Select(i=>i.ImageUrl)
           .FirstOrDefault()
           )).ToListAsync();

        public async Task<Category?> GetWithProductsAsync(int id)
        => await Table.Include(c => c.Products).ThenInclude(p => p.Images.OrderBy(i => i.Id).Take(1)).FirstOrDefaultAsync(i=>i.Id==id);
    }
}
