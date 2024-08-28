namespace mopo_flo_backend.Models.Profile;

public record ProfileModel(long Id, string Name, int Age, bool IsNewInPeriod, int PeriodCycleDuration, int BleedingDuration);