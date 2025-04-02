using violet.backend.Entities.Common;

namespace violet.backend.Entities;

public class Event
{
    public long Id { get; set; }
    public Guid AggregateId { get; set; }
    public string AggregateType { get; set; }
    public string DomainEventType { get; set; }
    public string EventData { get; set; }
    public int Version { get; set; }
    public DateTime TimeStamp { get; set; }
}