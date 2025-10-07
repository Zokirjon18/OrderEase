using OrderEase.Service.Services.Products.Models;

namespace OrderEase.Service.Services.Products
{
    public interface IProductService
    {
        Task<long> CreateAsync(ProductCreateModel model);
        Task UpdateAsync(long id, ProductUpdateModel model);
        Task ChangeStockAsync(long id, int stock);
        Task DeleteAsync(long id);
        Task<ProductViewModel> GetAsync(long id);
        Task<IEnumerable<ProductViewModel>> GetAllAsync(string category = null);
        Task<IEnumerable<ProductViewModel>> GetAllFinishedAsync();
    }
}
