namespace eat_not_waste_api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }

        // Navigation properties
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}