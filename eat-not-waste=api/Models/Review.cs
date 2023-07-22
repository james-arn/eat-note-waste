namespace eat_not_waste_api.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int MerchantId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public ICollection<Listing> Listings { get; set; }
        public Merchant Merchant { get; set; }
    }
}