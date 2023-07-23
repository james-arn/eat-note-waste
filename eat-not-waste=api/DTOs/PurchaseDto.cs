namespace eat_not_waste_api.DTOs
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ListingId { get; set; }
        public int Quantity { get; set; }
    }

}