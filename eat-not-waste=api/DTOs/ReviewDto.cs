namespace eat_not_waste_api.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int MerchantId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}