using System.Text;

namespace KSFramework.Utilities
{
    public static class RandomGenerator
    {
        #region Random Number Generator
        private static readonly Random _random = new Random();

        public static int GenerateRandomNumber()
        {
            return _random.Next();
        }
        public static int GenerateRandomNumber(int max)
        {
            return _random.Next(max);
        }
        public static int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public static int GenerateRandomNumberByCurrentTime()
        {
            var year = DateTime.Now.Year.ToString();
            var month = DateTime.Now.Month.ToString();
            var dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            var day = DateTime.Now.Day.ToString();
            var hour = DateTime.Now.Hour.ToString();
            var minutes = DateTime.Now.Minute.ToString();
            var second = DateTime.Now.Second.ToString();
            var milliSecond = DateTime.Now.Millisecond.ToString();

            var result = milliSecond + day + dayOfWeek + second + minutes + month + year + hour;
            return Int32.Parse(result);
        }



        #endregion

        #region Random String Generator
        public static string GenerateRandomString(int length)
        {
            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            return str_build.ToString();
        }
        #endregion
    }
}
