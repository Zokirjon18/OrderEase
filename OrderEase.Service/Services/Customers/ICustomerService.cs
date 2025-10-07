using OrderEase.Service.Services.Customers.Models;

namespace OrderEase.Service.Services.Customers
{
    public interface ICustomerService
    {
        Task<long> CreateAsync(CustomerCreateModel model);
        Task UpdateAsync(long id, CustomerUpdateModel model);
        Task DeleteAsync(long id);
        Task<CustomerViewModel> GetAsync(long id);
        Task<List<CustomerViewModel>> GetAllAsync(string Name = null, string phone = null, string email = null);
    }
}
