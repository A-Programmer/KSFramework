using System.Net.Mail;
using System.Text.RegularExpressions;

namespace KSFramework.Utilities
{
    public static class ValidationExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidMobile(this string phone)
        {
            return Regex.Match(phone, @"(\+98|0)?9\d{9}").Success;
        }

    }
}
