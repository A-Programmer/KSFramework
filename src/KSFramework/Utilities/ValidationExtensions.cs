using System.Net.Mail;
using System.Text.RegularExpressions;

namespace KSFramework.Utilities;
public static class ValidationExtensions
{
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
        try
        {
            _ = new MailAddress(email);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static bool IsValidMobile(this string phone)
    {
        return Regex.IsMatch(phone, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}");
    }

}