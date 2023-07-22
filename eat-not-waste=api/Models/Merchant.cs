namespace eat_not_waste_api.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public ICollection<Listing> Listings { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}