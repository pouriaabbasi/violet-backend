using violet.backend.Enums;

namespace violet.backend.Models.Auth;

public record UserModel(
    Guid Id,
    long TelegramId,
    bool IsBot,
    string FirstName,
    string LastName,
    string Username,
    string LanguageCode,
    bool IsPremium,
    bool AddedToAttachmentMenu,
    bool AllowsWriteToPm,
    string PhotoUrl,
    GenderType? Gender);