using violet.backend.Entities.Common;

namespace violet.backend.Entities;

public sealed class Period : BaseEntity
{
    public DateTime StartDayOfPeriod { get; set; }
    public DateTime? EndDayOfBleeding { get; set; }
}