using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.DataAccess.Concrete.EfCore;

public class EfCoreCartDal(EticaretContext context)
    : EfCoreGenericRepository<Cart>(context), ICartDal
{
    // Include + ThenInclude: Sepet → Kalemler → Ürün → (ilk) Resim zinciri tek seferde yüklenir.
    public async Task<Cart?> GetByUserIdAsync(string userId)
        => await Table
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p!.Images.OrderBy(img => img.Id).Take(1))
            .FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<int> GetItemCountAsync(string userId)
        => await Context.CartItems
            .Where(i => i.Cart!.UserId == userId)
            .SumAsync(i => i.Quantity);

    // NOT (EF Core 7+ ExecuteDeleteAsync): Kayıtları belleğe almadan doğrudan
    // "DELETE FROM CartItems WHERE CartId = ..." çalıştırır. Eskiden ExecuteSqlRaw ile elle SQL yazıyorduk.
    public async Task ClearAsync(int cartId)
        => await Context.CartItems
            .Where(i => i.CartId == cartId)
            .ExecuteDeleteAsync();
}
