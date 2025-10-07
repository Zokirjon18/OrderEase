using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Repositories;
using OrderEase.Domain.Entitites;
using OrderEase.Domain.Enums;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Orders.Models;

namespace OrderEase.Service.Services.Orders
{
    public class OrderService(IRepository<Order> orderRepository,
        IRepository<Customer> customerRepository,
        IRepository<OrderDetail> orderDetailRepository,
        IRepository<Product> productRepository) : IOrderService
    {
        public async Task<long> CreateAsync(OrderCreateModel model)
        {
            _ = await customerRepository.SelectAsync(model.CustomerId)
                ?? throw new NotFoundException($"No customer was found with ID = {model.CustomerId}");

            decimal totalAmount = 0;

            var createdOrder = await orderRepository.InsertAsync(new Order
            {
                CustomerId = model.CustomerId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                TotalAmount = totalAmount
            });

            foreach (var detail in model.Details)
            {
                var product = await productRepository.SelectAsync(detail.ProductId)
                    ?? throw new NotFoundException($"No product was found with ID = {detail.ProductId}");

                totalAmount += product.UnitPrice * detail.Quantity;
                product.Stock -= detail.Quantity;

                await orderDetailRepository.InsertAsync(new OrderDetail
                {
                    OrderId = createdOrder.Id,
                    LineTotal = detail.Quantity * product.UnitPrice,
                    Quantity = detail.Quantity,
                    ProductId = detail.ProductId,
                });
            }

            createdOrder.TotalAmount = totalAmount;
            await orderRepository.UpdateAsync(createdOrder);

            return createdOrder.Id;
        }

        public async Task CancelAsync(long id)
        {
            var orderForCancellation = await orderRepository.SelectAsync(id)
                ?? throw new NotFoundException($"No order was found with ID = {id}");

            if (orderForCancellation.Status != OrderStatus.Pending)
                throw new ArgumentIsNotValidException($"Order with ID = {id} cannot be cancelled.");

            var relatedOrderDetails = await orderDetailRepository
                .SelectAllAsQueryable()
                .Where(od => od.OrderId == id)
                .ToListAsync();

            foreach (var detail in relatedOrderDetails)
            {
                await orderDetailRepository.DeleteAsync(detail);
                var product = await productRepository.SelectAsync(detail.ProductId);

                product.Stock += detail.Quantity;
            }

            orderForCancellation.Status = OrderStatus.Cancelled;
            await orderRepository.DeleteAsync(orderForCancellation);
        }

        public async Task<OrderViewModel> GetAsync(long id)
        {
            return await orderRepository
                .SelectAllAsQueryable()
                .Include(o => o.Customer)
                .Include(o => o.Details)
                .ThenInclude(od => od.Product)
                .Where(order => order.Id == id && !order.IsDeleted)
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    Customer = new OrderViewModel.CustomerInfo
                    {
                        Id = o.Customer.Id,
                        FirstName = o.Customer.FirstName,
                        LastName = o.Customer.LastName
                    },
                    Details = o.Details.Select(od => new OrderViewModel.DetailInfo
                    {
                        Id = od.Id,
                        Quantity = od.Quantity,
                        LineTotal = od.LineTotal,
                        Product = new OrderViewModel.DetailInfo.ProductInfo
                        {
                            Id = od.Product.Id,
                            Name = od.Product.Name,
                            UnitPrice = od.Product.UnitPrice
                        }
                    })
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(long? customerId = null, OrderStatus? status = null)
        {
            var query = orderRepository
                .SelectAllAsQueryable()
                .Include(o => o.Customer)
                .Include(o => o.Details)
                .ThenInclude(od => od.Product)
                .Where(order => !order.IsDeleted)
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    Customer = new OrderViewModel.CustomerInfo
                    {
                        Id = o.Customer.Id,
                        FirstName = o.Customer.FirstName,
                        LastName = o.Customer.LastName
                    },
                    Details = o.Details.Select(od => new OrderViewModel.DetailInfo
                    {
                        Id = od.Id,
                        Quantity = od.Quantity,
                        LineTotal = od.LineTotal,
                        Product = new OrderViewModel.DetailInfo.ProductInfo
                        {
                            Id = od.Product.Id,
                            Name = od.Product.Name,
                            UnitPrice = od.Product.UnitPrice
                        }
                    })
                });

            if (customerId is not null)
            {
                query = query.Where(o => o.Customer.Id == customerId);   
            }

            if(status is not null)
            {
                query = query.Where(o => o.Status == status);
            }

            return await query.ToListAsync();
        }
    }
}
