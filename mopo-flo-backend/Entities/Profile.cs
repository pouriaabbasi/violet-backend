using System.ComponentModel.DataAnnotations;
using mopo_flo_backend.Entities.Common;
using mopo_flo_backend.Enums;

namespace mopo_flo_backend.Entities;

public class Profile : BaseEntity
{
    public long TelegramUserId { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    public int Age { get; set; }
    public GenderType Gender { get; set; }
    public bool IsNewInPeriod { get; set; }
    public int PeriodCycleDuration { get; set; }
    public int BleedingDuration { get; set; }

    public virtual TelegramUser TelegramUser { get; set; }
}