using System.ComponentModel.DataAnnotations;
using mopo_flo_backend.Entities.Common;

namespace mopo_flo_backend.Entities;

public class TelegramUser : BaseEntity
{
    public long TelegramId { get; set; }
    public bool IsBot { get; set; }
    [MaxLength(200)]
    public string FirstName { get; set; }
    [MaxLength(200)]
    public string LastName { get; set; }
    [MaxLength(200)]
    public string Username { get; set; }
    [MaxLength(50)]
    public string LanguageCode { get; set; }
    public bool IsPremium { get; set; }
    public bool AddedToAttachmentMenu { get; set; }
    public bool AllowsWriteToPm { get; set; }
    [MaxLength(500)]
    public string PhotoUrl { get; set; }

    public virtual ICollection<PeriodLog> PeriodLogs { get; set; }
}