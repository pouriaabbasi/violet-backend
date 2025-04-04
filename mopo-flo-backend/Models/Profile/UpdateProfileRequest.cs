using violet.backend.Enums;

namespace violet.backend.Models.Profile;

public record UpdateProfileRequest(string Name, int BirthYear, int PeriodCycleDuration, int BleedingDuration, int Height, decimal Weigh, GenderType Gender);