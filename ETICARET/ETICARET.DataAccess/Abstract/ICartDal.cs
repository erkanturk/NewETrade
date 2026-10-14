using ETICARET.Entities;

namespace ETICARET.DataAccess.Abstract;

public interface ICartDal : IRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(string userId);
    Task<int> GetItemCountAsync(string userId);
    Task ClearAsync(int cartId);
}
