using mopo_flo_backend.Services.Common;

namespace mopo_flo_backend.Models.Period;

public record PeriodLogModel(long Id, DateTime StartDayOfPeriod)
{
    public string StartDayOfPeriodPersian => StartDayOfPeriod.ToJalaliDate();
}