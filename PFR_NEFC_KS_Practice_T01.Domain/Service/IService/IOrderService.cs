using PFR_NEFC_KS_Practice_T01.Infrastructure.Entity;

namespace PFR_NEFC_KS_Practice_T01.Domain.Service.IService
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
