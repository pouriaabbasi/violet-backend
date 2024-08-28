namespace mopo_flo_backend.Models.Profile;

public record UpdateProfileRequest(string Name, int Age, bool IsNewInPeriod, int PeriodCycleDuration, int BleedingDuration);