using OrderEase.Domain.Enums;
using OrderEase.Service.Services.Orders.Models;

namespace OrderEase.Service.Services.Orders
{
    public interface IOrderService
    {
        Task<long> CreateAsync(OrderCreateModel model);
        Task CancelAsync(long id);
        Task<OrderViewModel> GetAsync(long id);
        Task<IEnumerable<OrderViewModel>> GetAllAsync(long? customerId = null, OrderStatus? status = null);    
    }
}
