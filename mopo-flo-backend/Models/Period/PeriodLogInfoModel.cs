using mopo_flo_backend.Services.Common;

namespace mopo_flo_backend.Models.Period;

public record PeriodLogInfoModel(long Id, DateTime StartDayOfPeriod, DateTime? EndDayOfBleeding)
{
    public string StartDayOfPeriodPersian => StartDayOfPeriod.ToJalaliDate();
    public string EndDayOfBleedingPersian => EndDayOfBleeding?.ToJalaliDate();
}