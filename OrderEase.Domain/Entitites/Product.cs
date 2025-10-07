namespace OrderEase.Domain.Entitites
{
    public class Product : Auditable
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
