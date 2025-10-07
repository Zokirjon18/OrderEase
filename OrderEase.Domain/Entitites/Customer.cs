namespace OrderEase.Domain.Entitites
{
    public class Customer : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
