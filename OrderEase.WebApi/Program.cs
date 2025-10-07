using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Contexts;
using OrderEase.DataAccess.Repositories;
using OrderEase.Service.Services.Customers;
using OrderEase.Service.Services.Orders;
using OrderEase.Service.Services.Products;
using OrderEase.Service.Services.Reports;

namespace OrderEase.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(option => 
            option.UseNpgsql(builder.Configuration.GetConnectionString("PosgreSqlConnection")));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddTransient<IReportService, ReportService>();

            var app = builder.Build();

            app.UseSwagger();

            app.UseSwaggerUI();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
