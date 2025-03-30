using violet.backend.Events.Common;
using violet.backend.Models.Auth;

namespace violet.backend.Events;

public class UpdateTelegramInfoDomainEvent : DomainEvent
{
    public TelegramInfoDto Data { get; set; }

    public static UpdateTelegramInfoDomainEvent Create(Guid userId, TelegramInfoDto data)
    {
        return new UpdateTelegramInfoDomainEvent { AggregateId = userId, Data = data };
    }
}