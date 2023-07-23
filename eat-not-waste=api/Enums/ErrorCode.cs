namespace eat_not_waste_api.Enums
{
    public enum ErrorCode
    {
        // General errors
        InvalidInput = 100,

        // Listing related errors
        ListingNotFound = 200,
        ListingExpired = 201,

        // Purchase related errors
        OutOfStock = 300,

        // User related errors
        EmailExists = 400,
        PasswordDoesNotMeetRequirements = 401,
        UserAuthenticationFailed = 402,
        InvalidEmail= 403,
        // Add other error codes here
    }

}
