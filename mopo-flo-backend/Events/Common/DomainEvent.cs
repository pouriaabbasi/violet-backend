namespace violet.backend.Events.Common;

public class DomainEvent<T>
{
    public Guid AggregateId { get; set; }
    public string DomainEventType { get; set;}
    public T Data { get; set; }
}