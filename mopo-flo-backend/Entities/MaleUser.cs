using violet.backend.Models.Profile;

namespace violet.backend.Entities
{
    public sealed class MaleUser : User
    {
        public MaleUser() { }

        public MaleUser(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            TelegramInfo = new TelegramInfo(user.TelegramInfo);
            PartnerUserId = user.PartnerUserId;
        }

        public MaleProfile MaleProfile { get; set; } = new();

        public void UpdateProfile(UpdateProfileRequest profile)
        {
            MaleProfile.Height  = profile.Height;
            MaleProfile.Weigh = profile.Weigh;
            MaleProfile.BirthYear = profile.BirthYear;
            MaleProfile.Name = profile.Name;
        }
    }
}
