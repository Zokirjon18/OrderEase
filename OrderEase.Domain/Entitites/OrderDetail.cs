namespace OrderEase.Domain.Entitites
{
    public class OrderDetail : Auditable
    {
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
