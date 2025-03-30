using System.ComponentModel.DataAnnotations;
using violet.backend.Entities.Common;

namespace violet.backend.Entities;

public sealed class TelegramInfo : BaseEntity
{
    public long TelegramId { get; set; }
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