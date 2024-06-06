namespace Fina.Core.Common
{
    public static class DateTimeExtensions
    {
        public static DateTime GetFirstDay(this DateTime dateTime, int? year = null, int? month = null)
        => new(year ?? dateTime.Year, month ?? dateTime.Month, 1);


        public static DateTime GetLastDay(this DateTime date, int? year = null, int? month = null)
        {
             var newDate = new DateTime(year ?? date.Year, month ?? date.Month, 1);

            return newDate.AddMonths(1)
                            .AddDays(-1);
        }
    }
}
