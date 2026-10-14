using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.DataAccess.Concrete.EfCore;

public class EfCoreOrderDal(EticaretContext context)
    : EfCoreGenericRepository<Order>(context), IOrderDal
{
    public async Task<List<Order>> GetByUserAsync(string userId)
        => await Table.AsNoTracking()
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p!.Images.OrderBy(img => img.Id).Take(1))
            .OrderByDescending(o => o.Id)
            .ToListAsync();

    public async Task<List<Order>> GetAllWithItemsAsync()
        => await Table.AsNoTracking()
            .Include(o => o.Items)
            .OrderByDescending(o => o.Id)
            .ToListAsync();

    public async Task<List<Order>> GetLatestAsync(int count)
        => await Table.AsNoTracking()
            .Include(o => o.Items)
            .OrderByDescending(o => o.Id)
            .Take(count)
            .ToListAsync();

    public async Task<Order?> GetDetailsAsync(int id)
        => await Table
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p!.Images.OrderBy(img => img.Id).Take(1))
            .FirstOrDefaultAsync(o => o.Id == id);

   
    public async Task<decimal> GetTotalRevenueAsync()
        => await Context.OrderItems
            .Where(i => i.Order!.State != OrderState.Cancelled)
            .SumAsync(i => i.Price * i.Quantity);
}
