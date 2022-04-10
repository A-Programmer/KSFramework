using System.Globalization;

namespace KSFramework.Utilities
{
    public static class DateTimeUtilityExtensions
    {
        public static string ToPersianDate(this DateTime dateTime)
        {
            if (dateTime.Equals(DateTime.MinValue))
                return "-";
            var persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1}/{2}",
                persianCalendar.GetYear(dateTime),
                persianCalendar.GetMonth(dateTime).ToString("00"),
                persianCalendar.GetDayOfMonth(dateTime).ToString("00"));
        }
        public static string ToPersianDate(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return "-";
            if (dateTime.Equals(DateTime.MinValue))
                return "-";
            var persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1}/{2}",
                persianCalendar.GetYear(dateTime.Value),
                persianCalendar.GetMonth(dateTime.Value).ToString("00"),
                persianCalendar.GetDayOfMonth(dateTime.Value).ToString("00"));
        }


        public static string ToPersianDate(this DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset.Equals(DateTimeOffset.MinValue))
                return "-";
            var dateTime = dateTimeOffset.DateTime;
            if (dateTime.Equals(DateTime.MinValue))
                return "-";
            var persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1}/{2}",
                persianCalendar.GetYear(dateTime),
                persianCalendar.GetMonth(dateTime).ToString("00"),
                persianCalendar.GetDayOfMonth(dateTime).ToString("00"));
        }
        public static string ToPersianDate(this DateTimeOffset? dateTimeOffset)
        {
            if (!dateTimeOffset.HasValue)
                return "-";
            if (dateTimeOffset.Equals(DateTimeOffset.MinValue))
                return "-";
            var dateTime = dateTimeOffset.Value.DateTime;
            if (dateTime.Equals(DateTime.MinValue))
                return "-";
            var persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1}/{2}",
                persianCalendar.GetYear(dateTime),
                persianCalendar.GetMonth(dateTime).ToString("00"),
                persianCalendar.GetDayOfMonth(dateTime).ToString("00"));
        }

        public static string ToPersianDateTime(this DateTime dateTime)
        {
            if (dateTime.Equals(DateTime.MinValue))
                return "-";
            var persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                persianCalendar.GetYear(dateTime),
                persianCalendar.GetMonth(dateTime).ToString("00"),
                persianCalendar.GetDayOfMonth(dateTime).ToString("00"),
                dateTime.Hour.ToString("00"),
                dateTime.Minute.ToString("00"),
               dateTime.Second.ToString("00"));
        }
        public static string ToPersianDateTime(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return "-";
            if (dateTime.Equals(DateTime.MinValue))
                return "-";
            var persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                persianCalendar.GetYear(dateTime.Value),
                persianCalendar.GetMonth(dateTime.Value).ToString("00"),
                persianCalendar.GetDayOfMonth(dateTime.Value).ToString("00"),
                dateTime.Value.Hour.ToString("00"),
                dateTime.Value.Minute.ToString("00"),
               dateTime.Value.Second.ToString("00"));
        }


        public static string ToPersianDateTime(this DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset.Equals(DateTimeOffset.MinValue))
                return "-";
            var dateTime = dateTimeOffset.DateTime;
            if (dateTime.Equals(DateTime.MinValue))
                return "-";
            var persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                persianCalendar.GetYear(dateTime),
                persianCalendar.GetMonth(dateTime).ToString("00"),
                persianCalendar.GetDayOfMonth(dateTime).ToString("00"),
                dateTime.Hour.ToString("00"),
                dateTime.Minute.ToString("00"),
               dateTime.Second.ToString("00"));
        }
        public static string ToPersianDateTime(this DateTimeOffset? dateTimeOffset)
        {
            if (!dateTimeOffset.HasValue)
                return "-";
            if (dateTimeOffset.Equals(DateTimeOffset.MinValue))
                return "-";
            var dateTime = dateTimeOffset.Value.DateTime;
            if (dateTime.Equals(DateTime.MinValue))
                return "-";
            var persianCalendar = new PersianCalendar();
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                persianCalendar.GetYear(dateTime),
                persianCalendar.GetMonth(dateTime).ToString("00"),
                persianCalendar.GetDayOfMonth(dateTime).ToString("00"),
                dateTime.Hour.ToString("00"),
                dateTime.Minute.ToString("00"),
               dateTime.Second.ToString("00"));
        }


        public static string TimeAgo(this DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return $"{years} {(years == 1 ? "year" : "years")} ago";
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return $"{months} {(months == 1 ? "month" : "months")} ago";
            }
            if (span.Days > 0)
                return $"{span.Days} {(span.Days == 1 ? "day" : "days")} ago";
            if (span.Hours > 0)
                return $"{span.Hours} {(span.Hours == 1 ? "hour" : "hours")} ago";
            if (span.Minutes > 0)
                return $"{span.Minutes} {(span.Minutes == 1 ? "minute" : "minutes")} ago";
            if (span.Seconds > 5)
                return $"{span.Seconds} seconds ago";
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }

        public static string ToConcatedDate(this DateTime date)
        {
            return date.Year + date.Month.ToString().PadLeft(2, '0') + date.Day.ToString().PadLeft(2, '0') +
                   date.Hour.ToString().PadLeft(2, '0') + date.Minute.ToString().PadLeft(2, '0') +
                   date.Second.ToString().PadLeft(2, '0');
        }

        public static DateTime FromConcatedDate(this string date)
        {
            return new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4, 2)),
                Convert.ToInt32(date.Substring(6, 2)), Convert.ToInt32(date.Substring(8, 2)),
                Convert.ToInt32(date.Substring(10, 2)), Convert.ToInt32(date.Substring(12, 2)));
        }

        public static DateTimeOffset ToDateTimeOffset(this string date)
        {
            //Input pattern: 1367-06-31 12:00:00

            var persianCalendar = new PersianCalendar();

            var dateTimeArray = date.Split(' ');
            var dateArray = dateTimeArray[0].Split('-');
            var year = 0000;
            var month = 00;
            var day = 00;
            var hour = 00;
            var minute = 00;
            var second = 00;
            year = Convert.ToInt32(dateArray[0]);
            month = Convert.ToInt32(dateArray[1]);
            day = Convert.ToInt32(dateArray[2]);

            if(dateTimeArray.Length > 1)
            {
                var timeArray = dateTimeArray[1].Split(':');
                hour = Convert.ToInt32(timeArray[0]);
                minute = Convert.ToInt32(timeArray[1]);
                second = Convert.ToInt32(timeArray[2]);
            }

            var dateTime = persianCalendar.ToDateTime(year, month, day, hour, minute, second,0);

            return new DateTimeOffset(dateTime);
        }



        public static DateTime GetDateTimeFromString(this string value)
        {
            if (!value.HasValue())
                throw new Exception("Date Format is invalid");
            try
            {
                //return DateTime.ParseExact(value ?? string.Empty, "ddd MMM dd yyyy", CultureInfo.InvariantCulture);
                return DateTime.Parse(value, new CultureInfo("it-IT"));
            }

            catch (Exception)
            {
                return DateTime.Parse(value ?? string.Empty, CultureInfo.InvariantCulture);
            }

        }

        public static DateTime GetDateTimeFromIsoString(this string value)
        {
            if (!value.HasValue()) throw new Exception("Date Format is invalid");
            return DateTime.ParseExact(value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture);
        }



        public static bool ConsideredDistance(this DateTime date, int distance)
        {
            return date > DateTime.Now.AddYears(distance);
        }

        public static bool HasLegalAge(this DateTime date)
        {
            return !ConsideredDistance(date, -18);
        }

        public static string ToCustomString(this DateTime date)
        {
            return date.ToString("ddd MMM dd yyyy", CultureInfo.InvariantCulture);
        }

        public static DateTime ToInvariantCulture(this DateTime date)
        {
            return DateTime.Parse(date.ToString(CultureInfo.InvariantCulture));
        }

        public static string ToShortTime(this DateTime date) => date.ToString("hh:mm tt");
    }
}
