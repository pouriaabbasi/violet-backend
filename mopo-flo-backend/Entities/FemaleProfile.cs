namespace violet.backend.Entities;

public sealed class FemaleProfile : Profile
{
    public bool IsNewInPeriod { get; set; }
    public int PeriodCycleDuration { get; set; }
    public int BleedingDuration { get; set; }
}