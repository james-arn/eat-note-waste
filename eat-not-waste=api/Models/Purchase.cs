namespace eat_not_waste_api.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ListingId { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Listing Listing { get; set; }

    }

}