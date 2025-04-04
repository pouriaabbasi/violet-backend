using violet.backend.Events.Common;
using violet.backend.Models.Profile;

namespace violet.backend.Events;

public class UpdateProfileDomainEvent : DomainEvent<UpdateProfileRequest>
{
    public static UpdateProfileDomainEvent Create(Guid userId, UpdateProfileRequest data)
    {
        return new UpdateProfileDomainEvent
        {
            AggregateId = userId,
            DomainEventType = nameof(UpdateProfileDomainEvent),
            Data = data,
        };
    }
}