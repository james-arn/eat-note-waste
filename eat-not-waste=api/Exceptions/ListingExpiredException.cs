using eat_not_waste_api.Enums;

namespace eat_not_waste_api.Exceptions
{
    public class ListingExpiredException : ServiceException
    {
        public ListingExpiredException()
            : base(ErrorCode.ListingExpired, "The listing has expired.")
        {
        }
    }
}
