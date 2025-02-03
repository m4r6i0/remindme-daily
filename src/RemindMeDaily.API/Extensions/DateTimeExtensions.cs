using System;

namespace RemindMeDaily.API.Extensions
{
    public static class DateTimeExtensions
    {
        // Método de extensão para formatar DateTime
        public static string ToFormattedString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static DateTime ToADateTime(this string dateTime)
        {
            return DateTime.Parse(dateTime);
        }

        // Método de extensão para verificar se é fim de semana
        public static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
