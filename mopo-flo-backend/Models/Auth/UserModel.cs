using Newtonsoft.Json;

namespace mopo_flo_backend.Models.Auth;

public record UserModel(
    long Id,
    int TelegramId,
    bool IsBot,
    string FirstName,
    string LastName,
    string Username,
    string LanguageCode,
    bool IsPremium,
    bool AddedToAttachmentMenu,
    bool AllowsWriteToPm,
    string PhotoUrl);