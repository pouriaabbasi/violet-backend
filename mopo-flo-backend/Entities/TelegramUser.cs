using mopo_flo_backend.Entities.Common;

namespace mopo_flo_backend.Entities;

public class TelegramUser : BaseEntity
{
    public int TelegramId { get; set; }
    public bool IsBot { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string LanguageCode { get; set; }
    public bool IsPremium { get; set; }
    public bool AddedToAttachmentMenu { get; set; }
    public bool AllowsWriteToPm { get; set; }
    public string PhotoUrl { get; set; }
}