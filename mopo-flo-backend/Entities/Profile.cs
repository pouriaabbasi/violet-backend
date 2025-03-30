using System.ComponentModel.DataAnnotations;
using violet.backend.Entities.Common;
using violet.backend.Enums;

namespace violet.backend.Entities;

public sealed class Profile : BaseEntity
{
    public string Name { get; set; }
    public int Age { get; set; }
    public GenderType Gender { get; set; }
    public bool IsNewInPeriod { get; set; }
    public int PeriodCycleDuration { get; set; }
    public int BleedingDuration { get; set; }
}