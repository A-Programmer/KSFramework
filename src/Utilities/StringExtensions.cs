using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using KSFramework.Enums;
using KSFramework.Exceptions;

namespace KSFramework.Utilities
{
    public static class StringExtensions
    {
        #region Card Mask Creator
        public static string Mask(this string source, int start, int maskLength)
        {
            return source.Mask(start, maskLength, '*');
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            try
            {
                T enumValue;
                if (!Enum.TryParse(value, true, out enumValue))
                {
                    return defaultValue;
                }
                return enumValue;
            }
            catch (Exception)
            {
                throw new AppException(ApiResultStatusCode.ServerError, "آیتم مورد نظر یافت نشد", HttpStatusCode.InternalServerError);
            }
        }

        public static string Mask(this string source, int start, int maskLength, char maskCharacter)
        {
            if (start > source.Length - 1)
            {
                throw new ArgumentException("Start position is greater than string length");
            }

            if (maskLength > source.Length)
            {
                throw new ArgumentException("Mask length is greater than string length");
            }

            if (start + maskLength > source.Length)
            {
                throw new ArgumentException("Start position and mask length imply more characters than are present");
            }

            string mask = new string(maskCharacter, maskLength);
            string unMaskStart = source.Substring(0, start);
            string unMaskEnd = source.Substring(start + maskLength, source.Length - maskLength);

            return unMaskStart + mask + unMaskEnd;
        }
        #endregion

        #region Get Last n chars in String
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
        #endregion


        public static string ToSnakeCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }

        public static string ToNumeric(this int value)
        {
            return value.ToString("N0"); //"123,456"
        }

        public static string ToNumeric(this decimal value)
        {
            return value.ToString("N0");
        }

        public static string ToCurrency(this int value)
        {
            //fa-IR => current culture currency symbol => ریال
            //123456 => "123,123ریال"
            return value.ToString("C0");
        }

        public static string ToCurrency(this decimal value)
        {
            return value.ToString("C0");
        }

        #region Separate Dgits
        public static string SeparateDigits(this int number)
        {
            return number.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = "،"
            });
        }

        public static string SeparateDigits(this double number)
        {
            return number.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = "،"
            });
        }
        #endregion

        public static string En2Fa(this string str)
        {
            return str.Replace("0", "۰")
                .Replace("1", "۱")
                .Replace("2", "۲")
                .Replace("3", "۳")
                .Replace("4", "۴")
                .Replace("5", "۵")
                .Replace("6", "۶")
                .Replace("7", "۷")
                .Replace("8", "۸")
                .Replace("9", "۹");
        }

        public static string Fa2En(this string str)
        {
            return str.Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                //iphone numeric
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9");
        }

        public static string FixPersianChars(this string str)
        {
            return str.Replace("ﮎ", "ک")
                .Replace("ﮏ", "ک")
                .Replace("ﮐ", "ک")
                .Replace("ﮑ", "ک")
                .Replace("ك", "ک")
                .Replace("ي", "ی")
                .Replace(" ", " ")
                .Replace("‌", " ")
                .Replace("ھ", "ه");//.Replace("ئ", "ی");
        }

        public static string CleanString(this string str)
        {
            return str.Trim().FixPersianChars().Fa2En().NullIfEmpty();
        }

        #region Helpers
        public static String CreateSlug(this string key)
        {
            return GenerateSlug(key);
        }

        private static string GenerateSlug(string title)
        {
            var slug = RemoveAccent(title).ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
            slug = Regex.Replace(slug, @"\s+", "-").Trim();
            slug = Regex.Replace(slug, @"-+", "-");

            return slug.Trim();
        }
        private static string RemoveAccent(string text)
        {
            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Returns only the first n characters of a String.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string TruncateString(this string str, int start, int maxLength)
        {
            return str.Substring(start, Math.Min(str.Length, maxLength));
        }
        #endregion

        public static string NullIfEmpty(this string str)
        {
            return str?.Length == 0 ? null : str;
        }
    }
}
