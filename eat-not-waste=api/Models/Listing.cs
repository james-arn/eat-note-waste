namespace eat_not_waste_api.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }

        // Navigation properties
        public Merchant Merchant { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

    }

}