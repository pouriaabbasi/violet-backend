using System.Globalization;

namespace mopo_flo_backend.Services.Common;

public static class DateExtension
{
    public static string ToJalaliDate(this DateTime value)
    {
        var pc = new PersianCalendar();

        return $"{pc.GetYear(value):D4}/{pc.GetMonth(value):D2}/{pc.GetDayOfMonth(value):D2}";
    }

    public static string ToJalaliDateTime(this DateTime value)
    {
        var jalaliDate = value.ToJalaliDate();

        return $"{jalaliDate} {value.Hour:D2}:{value.Minute:D2}:{value.Second:D2}";
    }

    public static DateTime? ToGregorianDate(this string value)
    {
        try
        {
            if (value == null) return null;

            var pc = new PersianCalendar();
            var parts = value.Split("-");

            return pc.ToDateTime(
                Convert.ToInt32(parts[0]),
                Convert.ToInt32(parts[1]),
                Convert.ToInt32(parts[2]), 0, 0, 0, 0);
        }
        catch
        {
            return null;
        }
    }
}