using violet.backend.Enums;

namespace violet.backend.Models.Profile;

public record ProfileDto(
    string Name,
    int BirthYear,
    int Height,
    Decimal Weigh,
    int PeriodCycleDuration,
    int BleedingDuration,
    GenderType Gender)
{
    public const int DefaultPeriodCycleDuration = 28;
    public const int DefaultBleedingDuration = 5;
}