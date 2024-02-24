namespace Nik.Common;

public static class DateOnlyExtension
{
    public static DateOnly Now(this DateOnly _)
    {
        return System.DateOnly.FromDateTime(DateTime.Now);
    }

    public static DateOnly DateOnly(this DateTime dateTime)
    {
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}