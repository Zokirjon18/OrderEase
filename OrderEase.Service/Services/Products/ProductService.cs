using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Repositories;
using OrderEase.Domain.Entitites;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Products.Models;

namespace OrderEase.Service.Services.Products
{
    public class ProductService(IRepository<Product> productRepository) : IProductService
    {
        public async Task<long> CreateAsync(ProductCreateModel model)
        {
            var createdProduct = await productRepository.InsertAsync(new Product
            {
                Name = model.Name,
                Category = model.Category,
                UnitPrice = model.UnitPrice,
                Stock = model.Stock
            });

            return createdProduct.Id;
        }

        public async Task UpdateAsync(long id, ProductUpdateModel model)
        {
            var productForUpdation = await productRepository.SelectAsync(id)
                ?? throw new NotFoundException($"No product was found with the ID = {id}");

            productForUpdation.Name = model.Name;
            productForUpdation.Category = model.Category;
            productForUpdation.UnitPrice = model.UnitPrice;
            productForUpdation.Stock = model.Stock;

            await productRepository.UpdateAsync(productForUpdation);
        }

        public async Task ChangeStockAsync(long id, int stock)
        {
            var product = await productRepository.SelectAsync(id)
                ?? throw new NotFoundException($"No product was found with the ID = {id}");

            product.Stock = stock;

            await productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(long id)
        {
            var productForDeletion = await productRepository.SelectAsync(id)
                ?? throw new NotFoundException($"No product was found with the ID = {id}");

            await productRepository.DeleteAsync(productForDeletion);
        }

        public async Task<ProductViewModel> GetAsync(long id)
        {
            var product = await productRepository.SelectAsync(id)
                ?? throw new NotFoundException($"No product was found with the ID = {id}");

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock
            };
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllFinishedAsync()
        {
             return await productRepository
                .SelectAllAsQueryable()
                .Where(product => !product.IsDeleted && product.Stock == 0)
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category,
                    UnitPrice = product.UnitPrice,
                    Stock = product.Stock
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(string category = null)
        {
            var query = productRepository
                .SelectAllAsQueryable()
                .Where(product => !product.IsDeleted);

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p => p.Category.ToLower().Contains(category.ToLower()));    
            }

            return await query
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category,
                    UnitPrice = product.UnitPrice,
                    Stock = product.Stock
                }).ToListAsync();
        }
    }
}
