using mopo_flo_backend.Entities.Common;

namespace mopo_flo_backend.Entities;

public class PeriodLog : BaseEntity
{
    public long TelegramUserId { get; set; }
    public DateTime StartDayOfPeriod { get; set; }
    public DateTime? EndDayOfBleeding { get; set; }


    public virtual TelegramUser TelegramUser { get; set; }
}