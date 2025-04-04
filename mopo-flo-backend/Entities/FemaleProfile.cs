namespace violet.backend.Entities;

public sealed class FemaleProfile : Profile
{
    public int PeriodCycleDuration { get; set; }
    public int BleedingDuration { get; set; }
}