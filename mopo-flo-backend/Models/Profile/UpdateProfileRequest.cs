using violet.backend.Enums;

namespace violet.backend.Models.Profile;

public record UpdateProfileRequest(string Name, int Age, bool IsNewInPeriod, int PeriodCycleDuration, int BleedingDuration, GenderType Gender);