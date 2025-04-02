using violet.backend.Events.Common;
using violet.backend.Models.Auth;

namespace violet.backend.Events;

public class UpdateTelegramInfoDomainEvent : DomainEvent<TelegramInfoDto>
{
    public static UpdateTelegramInfoDomainEvent Create(Guid userId, TelegramInfoDto data)
    {
        return new UpdateTelegramInfoDomainEvent
        {
            AggregateId = userId,
            DomainEventType = nameof(UpdateTelegramInfoDomainEvent),
            Data = data,
        };
    }
}