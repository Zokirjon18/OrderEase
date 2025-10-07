using OrderEase.Domain.Entitites;

namespace OrderEase.Service.Services.Reports.Models
{
    public class ReportModel
    {
        public IEnumerable<ProductInfo> Products { get; set; }
        public IEnumerable<CustomerInfo> Customers { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrder { get; set; }
        public DateOnly Month { get; set; }

        public class ProductInfo
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
        }

        public class CustomerInfo
        {
            public long Id { get; set; }
            public string FullName { get; set; }
            public int Quantity { get; set; }
        }
    }
}
