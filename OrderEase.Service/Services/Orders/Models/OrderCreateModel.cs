using OrderEase.Domain.Entitites;

namespace OrderEase.Service.Services.Orders.Models
{
    public class OrderCreateModel
    {
        public long CustomerId { get; set; }
        public IEnumerable<OrderDetailInfo> Details { get; set; }

        public class OrderDetailInfo
        {
            public int Quantity { get; set; }
            public long ProductId { get; set; }
        }
    }
}
