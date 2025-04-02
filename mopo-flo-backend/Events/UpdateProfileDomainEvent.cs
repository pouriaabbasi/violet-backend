using violet.backend.Events.Common;
using violet.backend.Models.Auth;
using violet.backend.Models.Profile;

namespace violet.backend.Events;

public class UpdateProfileDomainEvent : DomainEvent<ProfileDto>
{
    public static UpdateProfileDomainEvent Create(Guid userId, ProfileDto data)
    {
        return new UpdateProfileDomainEvent
        {
            AggregateId = userId,
            DomainEventType = nameof(UpdateProfileDomainEvent),
            Data = data,
        };
    }
}