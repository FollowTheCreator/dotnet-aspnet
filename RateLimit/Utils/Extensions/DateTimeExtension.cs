using System;

namespace Utils.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToBirthdayString(this DateTime birthday)
        {
            return birthday.ToString("MMMM d, yyyy");
        }
    }
}
