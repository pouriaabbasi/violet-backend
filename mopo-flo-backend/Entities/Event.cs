using violet.backend.Entities.Common;

namespace violet.backend.Entities;

public class Event : BaseEntity
{
    public Guid AggregateId { get; set; }
    public string AggregateType { get; set; }
    public string DomainEventType { get; set; }
    public string EventData { get; set; }
    public int Version { get; set; }
}