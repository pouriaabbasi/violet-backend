using violet.backend.Entities.Common;
using violet.backend.Models.Auth;

namespace violet.backend.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool InitialSetup { get; set; }
        public TelegramInfo TelegramInfo { get; set; } = new();
        public Guid PartnerUserId { get; set; }

        public virtual void Apply(List<Event> events) { }
        public void UpdateTelegramInfo(TelegramInfoDto telegramInfo)
        {
            TelegramInfo.AddedToAttachmentMenu = telegramInfo.AddedToAttachmentMenu;
            TelegramInfo.AllowsWriteToPm = telegramInfo.AllowsWriteToPm;
            TelegramInfo.FirstName = telegramInfo.FirstName;
            TelegramInfo.IsBot = telegramInfo.IsBot;
            TelegramInfo.IsPremium = telegramInfo.IsPremium;
            TelegramInfo.LanguageCode = telegramInfo.LanguageCode;
            TelegramInfo.LastName = telegramInfo.LastName;
            TelegramInfo.PhotoUrl = telegramInfo.PhotoUrl;
            TelegramInfo.TelegramId = telegramInfo.Id;
            TelegramInfo.Username = telegramInfo.Username;
        }
    }
}
