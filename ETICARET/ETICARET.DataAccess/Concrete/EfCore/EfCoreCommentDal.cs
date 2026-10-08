using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class EfCoreCommentDal(EticaretContext context) : EfCoreGenericRepository<Comment>(context), ICommentDal
    {
        public async Task<List<Comment>> GetAllWithProductAsync()
       => await Table.AsNoTracking().Include(c => c.ProductId).OrderByDescending(c => c.Id).ToListAsync();

        public async Task<List<Comment>> GetByProductAsync(int productId)
       => await Table.AsNoTracking().Where(c => c.ProductId==productId).ToListAsync();

    }
}
