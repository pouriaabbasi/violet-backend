using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using violet.backend.Entities;
using violet.backend.Enums;
using violet.backend.Events;
using violet.backend.Events.Common;
using violet.backend.Models.Auth;

namespace violet.backend.Infrastructures.Repository;

public interface IUserRepository
{
    Task<User> GetUserFromTelegramId(long telegramId);
    Task<User> UpdateTelegramInfo(User userEntity, TelegramInfoDto telegramInfoDto);
}

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<User> GetUserFromTelegramId(long telegramId)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x =>
            x.TelegramInfo != null
            && x.TelegramInfo.TelegramId == telegramId);

        if (user == null) return null;

        return LoadUserFromEvents(user.Id);
    }

    public async Task<User> UpdateTelegramInfo(User userEntity, TelegramInfoDto telegramInfoDto)
    {
        userEntity.UpdateTelegramInfo(telegramInfoDto);
        dbContext.Users.Update(userEntity);
        await dbContext.SaveChangesAsync();

        await CreateEvent<UpdateTelegramInfoDomainEvent, TelegramInfoDto>(userEntity.Id, telegramInfoDto);

        return userEntity;
    }

    private async Task CreateEvent<T, TE>(Guid aggregateId, TE data) where T : DomainEvent<TE>, new()
    {
        var domainEvent = new T
        {
            AggregateId = aggregateId,
            Data = data
        };

        var entity = new Event
        {
            DomainEventType = nameof(T),
            AggregateId = domainEvent.AggregateId,
            AggregateType = GetType().Name,
            EventData = JsonSerializer.Serialize(domainEvent)
        };
        await dbContext.Events.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    private User LoadUserFromEvents(Guid userId)
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