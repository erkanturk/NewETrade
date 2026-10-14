using System.Linq.Expressions;
using ETICARET.Entities;

namespace ETICARET.DataAccess.Abstract;

/// <summary>
/// Generic Repository: Bütün tablolar için ortak CRUD (Create-Read-Update-Delete) işlemleri.
/// "where T : BaseEntity" → T sadece BaseEntity'den türeyen bir sınıf olabilir (generic kısıt).
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);

    // filter verilmezse hepsi gelir. Örnek: GetAllAsync(p => p.Stock > 0)
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

    Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
