using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ETICARET.DataAccess.Abstract
{
    /// <summary>
    /// Generic Repository : Bütün tablolar için ortak Crud (Cread Read Update Delete) işlemleri
    /// Where T :BaseEntity T sadece BaseEntityden türüyen bir sınıf olabilir generic kısıtlama
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T :BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);


    }
}
