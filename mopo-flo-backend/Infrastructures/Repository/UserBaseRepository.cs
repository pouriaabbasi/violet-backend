using System.Text.Json;
using violet.backend.Entities;
using violet.backend.Enums;
using violet.backend.Events;
using violet.backend.Events.Common;

namespace violet.backend.Infrastructures.Repository;

public interface IUserBaseRepository
{
    Task CreateEvent<T, TE>(Guid aggregateId, TE data) where T : DomainEvent<TE>, new();
    User LoadUserFromEvents(Guid userId);
}
public class UserBaseRepository(AppDbContext dbContext) : IUserBaseRepository
{
    public async Task CreateEvent<T, TE>(Guid aggregateId, TE data) where T : DomainEvent<TE>, new()
    {
        var domainEvent = new T
        {
            AggregateId = aggregateId,
            Data = data
        };

        var entity = new Event
        {
            DomainEventType = typeof(T).Name,
            AggregateId = domainEvent.AggregateId,
            EventData = JsonSerializer.Serialize(domainEvent)
        };
        await dbContext.Events.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public User LoadUserFromEvents(Guid userId)
    {
        var events = dbContext.Events.Where(x => x.AggregateId == userId).ToList();

        var user = new User();

        foreach (var @event in events)
        {
            switch (@event.DomainEventType)
            {
                case nameof(UpdateTelegramInfoDomainEvent):
                    var updateTelegramInfoDomainEvent = JsonSerializer.Deserialize<UpdateTelegramInfoDomainEvent>(@event.EventData);
                    user!.UpdateTelegramInfo(updateTelegramInfoDomainEvent.Data);
                    break;
                case nameof(UpdateProfileDomainEvent):
                    var updateProfileDomainEvent = JsonSerializer.Deserialize<UpdateProfileDomainEvent>(@event.EventData);
                    user = updateProfileDomainEvent.Data.Gender == GenderType.Female
                        ? user as FemaleUser
                        : user as MaleUser;
                    break;
            }
        }

        user!.Apply(events);

        return user;
    }
}