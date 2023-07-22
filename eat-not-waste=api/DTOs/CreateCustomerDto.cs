namespace eat_not_waste_api.DTOs
{
    public class CreateCustomerDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Preferences { get; set; }
    }
}