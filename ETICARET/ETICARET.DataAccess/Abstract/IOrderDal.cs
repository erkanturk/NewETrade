using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.DataAccess.Abstract
{
    public interface IOrderDal:IRepository<Order>
    {
        Task<List<Order>> GetByUserAsync(string userId);
        Task<List<Order>> GetAllWithItemsAsync();
        Task<List<Order>> GetLatestAsync(int count);
        Task<Order?> GetDetailsAsync(int id);
        Task<decimal> GetTotalRevenueAsync();
    }
}
