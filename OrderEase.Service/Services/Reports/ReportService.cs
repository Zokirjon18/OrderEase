using System.Globalization;
using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Repositories;
using OrderEase.Domain.Entitites;
using OrderEase.Service.Services.Reports.Models;

namespace OrderEase.Service.Services.Reports
{
    public class ReportService(
        IRepository<Order> orderRepository,
        IRepository<OrderDetail> orderDetailRepository) : IReportService
    {

        public async Task<ReportModel> CalculateMonthlyReportAsync()
        {
            DateOnly currentMonth = DateOnly.FromDateTime(DateTime.Now);

            // product grouping process for determining top ordered products
            var groupedOrderDetails = orderDetailRepository.SelectAllAsQueryable()
                .Include(od => od.Product)
                .Where(od => od.CreatedAt.Month == DateTime.Now.Month)
                .GroupBy(od => od.ProductId).ToList();

            List<ReportModel.ProductInfo> products = new();
            foreach (var group in groupedOrderDetails)
            {
                int quantityOfPerProduct = 0;

                group.ToList().ForEach(o =>
                {
                    quantityOfPerProduct += o.Quantity;
                });

                products.Add(new ReportModel.ProductInfo
                {
                    Id = group.Key,
                    Name = group.First().Product.Name,
                    Quantity = quantityOfPerProduct,
                });
            }


            // retreiving monthly orders for calculating totalOrders and revenues
            var monthlyOrders = await orderRepository.SelectAllAsQueryable()
                .Include(o => o.Customer)
                .Where(order => !order.IsDeleted && order.OrderDate.Month == DateTime.Now.Month)
                .ToListAsync();


            // customer retreiving process for finding top ordered customers
            var groupedOrdersByCustomerId = monthlyOrders.GroupBy(o => o.CustomerId);

            var monthlyCustomers = new List<ReportModel.CustomerInfo>();
            foreach(var group in groupedOrdersByCustomerId)
            {
                int quantityOfOrdersPerCustomer = group.ToList().Count();

                monthlyCustomers.Add(new ReportModel.CustomerInfo
                {
                    Id = group.Key,
                    FullName = group.First().Customer.FirstName + " " + group.First().Customer.LastName,
                    Quantity = quantityOfOrdersPerCustomer,
                });
            }


            // returning final result
            return new ReportModel
            {
                Month = currentMonth,
                TotalOrder = monthlyOrders.Count(),
                TotalRevenue = monthlyOrders.Sum(o => o.TotalAmount),
                Products = products.OrderByDescending(o => o.Quantity).Take(10),
                Customers = monthlyCustomers.OrderByDescending(c => c.Quantity).Take(10)
            };
        }
    }
}
