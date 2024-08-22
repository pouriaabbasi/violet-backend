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
}