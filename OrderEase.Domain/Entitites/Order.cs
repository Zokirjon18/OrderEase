using OrderEase.Domain.Enums;

namespace OrderEase.Domain.Entitites
{
    public class Order : Auditable
    {
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}
