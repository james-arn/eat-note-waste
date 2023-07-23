using eat_not_waste_api.Enums;

namespace eat_not_waste_api.Exceptions
{
    public class ServiceException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public ServiceException(ErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
