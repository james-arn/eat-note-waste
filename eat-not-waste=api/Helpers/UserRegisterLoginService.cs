namespace eat_not_waste_api.Helpers
{
    public class UserRegisterLoginService
    {
        public static bool PasswordMeetsRequirements(string password)
        {
            // The password must be at least 8 characters long,
            // contain at least one uppercase letter, one lowercase letter,
            // one digit, and one special character
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}