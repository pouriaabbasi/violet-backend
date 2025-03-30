using violet.backend.Enums;

namespace violet.backend.Models.Profile;

public record ProfileModel(
    Guid Id,
    string Name,
    int Age,
    bool IsNewInPeriod,
    int PeriodCycleDuration,
    int BleedingDuration,
    GenderType Gender)
{
    public const int DefaultPeriodCycleDuration = 28;
    public const int DefaultBleedingDuration = 5;
}