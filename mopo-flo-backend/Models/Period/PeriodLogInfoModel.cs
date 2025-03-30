using violet.backend.Services.Common;

namespace violet.backend.Models.Period;

public record PeriodLogInfoModel(Guid Id, DateTime StartDayOfPeriod, DateTime? EndDayOfBleeding)
{
    public string StartDayOfPeriodPersian => StartDayOfPeriod.ToJalaliDate();
    public string EndDayOfBleedingPersian => EndDayOfBleeding?.ToJalaliDate();
}