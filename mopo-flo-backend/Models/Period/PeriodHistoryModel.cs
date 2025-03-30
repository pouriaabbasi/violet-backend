using violet.backend.Services.Common;

namespace violet.backend.Models.Period;

public record PeriodHistoryModel(Guid Id, DateTime StartDate, DateTime? EndOfBleedingDate)
{
    public int Row { get; set; }
    public DateTime? StartDateOfNextPeriod { get; set; }
    public string StartDatePersian => StartDate.ToJalaliDate();
    public int? BleedingDuration => EndOfBleedingDate == null ? null : (EndOfBleedingDate.Value - StartDate).Days;
    public int? PeriodDuration => StartDateOfNextPeriod == null ? null : (StartDateOfNextPeriod.Value - StartDate).Days;
}
