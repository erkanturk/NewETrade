using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class EfCoreGenericRepository<TEntity>(EticaretContext context):IRepository<TEntity> where TEntity:BaseEntity
    {
        protected EticaretContext Context { get; } = context;
        protected DbSet<TEntity> Table => Context.Set<TEntity>();

        public virtual async Task AddAsync(TEntity entity)
        {
            Table.Add(entity);
            await Context.SaveChangesAsync();
        }


        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null)
        => await Table.AnyAsync(filter);

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        => filter is null ? await Table.CountAsync() : await Table.Where(filter).CountAsync();

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Table.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
       => filter is null ? await Table.ToListAsync() : await Table.Where(filter).ToListAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id)
      => await Table.FindAsync(id);

        public virtual async Task UpdateAsync(TEntity entity)
        {
            /* Change Trackng:Veritabanında okunmuş bir nesnede yapılan değişiklik ef zaten bilir saveChanges dememiz yeterlidir
             * Formdan yeni oluşturulmuş nesneyi update ile bildiririz.
             */
           if (Context.Entry(entity).State==EntityState.Detached)
            {
                Table.Update(entity);
            }
            await Context.SaveChangesAsync();
        }
    }
}
