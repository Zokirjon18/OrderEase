namespace OrderEase.Service.Services.Products.Models
{
    public class ProductUpdateModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
    }
}
