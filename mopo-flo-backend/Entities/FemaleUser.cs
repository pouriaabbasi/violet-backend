using System.Text.Json;
using violet.backend.Events;
using violet.backend.Models.Profile;

namespace violet.backend.Entities
{
    public sealed class FemaleUser : User
    {
        public FemaleUser() { }

        public FemaleUser(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            TelegramInfo = new TelegramInfo(user.TelegramInfo);
            PartnerUserId = user.PartnerUserId;
        }

        public FemaleProfile FemaleProfile { get; set; } = new();
        public List<Period> Periods { get; set; } = [];

        public void UpdateProfile(UpdateProfileRequest profile)
        {
            FemaleProfile.Height = profile.Height;
            FemaleProfile.Weigh = profile.Weigh;
            FemaleProfile.BleedingDuration = profile.BleedingDuration;
            FemaleProfile.PeriodCycleDuration = profile.PeriodCycleDuration;
            FemaleProfile.BirthYear = profile.BirthYear;
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
