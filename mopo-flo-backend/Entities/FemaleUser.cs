using System.Text.Json;
using violet.backend.Events;
using violet.backend.Models.Profile;

namespace violet.backend.Entities
{
    public sealed class FemaleUser : User
    {
        public FemaleProfile FemaleProfile { get; set; } = new();
        public List<Period> Periods { get; set; } = [];

        public void UpdateProfile(ProfileDto profile)
        {
            FemaleProfile.BleedingDuration = profile.BleedingDuration;
            FemaleProfile.IsNewInPeriod = profile.IsNewInPeriod;
            FemaleProfile.PeriodCycleDuration = profile.PeriodCycleDuration;
            FemaleProfile.Age = profile.Age;
            FemaleProfile.Name = profile.Name;
        }

        public override void Apply(List<Event> events)
        {
            foreach (var @event in events)
            {
                switch (@event.DomainEventType)
                {
                    case nameof(UpdateProfileDomainEvent):
                        var updateProfileDomainEvent = JsonSerializer.Deserialize<UpdateProfileDomainEvent>(@event.EventData);
                        UpdateProfile(updateProfileDomainEvent.Data);
                        break;
                }
            }
        }
    }
}
