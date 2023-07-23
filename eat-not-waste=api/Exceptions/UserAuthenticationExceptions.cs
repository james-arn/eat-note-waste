using eat_not_waste_api.Enums;

namespace eat_not_waste_api.Exceptions
{
    public class EmailExistsException : ServiceException
    {
        public EmailExistsException()
            : base(ErrorCode.EmailExists, "The specified email already exists.")
        {
        }
    }

    public class InvalidEmailException : ServiceException
    {
        public InvalidEmailException()
            : base(ErrorCode.InvalidEmail, "The specified email is not valid.")
        {
        }
    }

    public class PasswordDoesNotMeetRequirementsException : ServiceException
    {
        public PasswordDoesNotMeetRequirementsException()
            : base(ErrorCode.PasswordDoesNotMeetRequirements, "The provided password does not meet the minimum requirements.")
        {
        }
    }

    public class UserAuthenticationFailedException : ServiceException
    {
        public UserAuthenticationFailedException()
            : base(ErrorCode.UserAuthenticationFailed, "The email or password is incorrect.")
        {
        }
    }

}
