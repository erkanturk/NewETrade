using ETICARET.Business.Models;
using ETICARET.Entities;

namespace ETICARET.Business.Abstract;

public interface IOrderService
{
    Task<ServiceResult> PlaceOrderAsync(CheckoutRequest request);
    Task<List<Order>> GetUserOrdersAsync(string userId);

    // Yönetim paneli
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetLatestOrdersAsync(int count = 5);
    Task<Order?> GetOrderDetailsAsync(int id);
    Task<int> CountAsync();
    Task<decimal> GetTotalRevenueAsync();
    Task<ServiceResult> UpdateStateAsync(int orderId, OrderState state);
}
