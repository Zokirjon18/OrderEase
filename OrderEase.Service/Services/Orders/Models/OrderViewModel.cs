using OrderEase.Domain.Enums;

namespace OrderEase.Service.Services.Orders.Models
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<DetailInfo> Details { get; set; }
        public CustomerInfo Customer { get; set; }

        public class CustomerInfo
        {
            public long Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class DetailInfo
        {
            public long Id { get; set; }
            public int Quantity { get; set; }
            public decimal LineTotal { get; set; }
            public ProductInfo Product { get; set; }

            public class ProductInfo
            {
                public long Id { get; set; }
                public string Name { get; set; }
                public decimal UnitPrice { get; set; }
            }
        }

    }
}
