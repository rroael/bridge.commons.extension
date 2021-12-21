using System;

namespace Bridge.Commons.Extension
{
    public static class DateTimeExtension
    {
        public static int GetAge(this DateTime date, bool fromUtc = true)
        {
            var today = fromUtc ? DateTime.UtcNow.Date : DateTime.Now.Date;
            var age = today.Year - date.Year;
            if (date.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}