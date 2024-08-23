using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace mopo_flo_backend.Models.Auth;

public record TelegramUserModel(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("is_bot")] bool IsBot,
    [property: JsonPropertyName("first_name")] string FirstName,
    [property: JsonPropertyName("last_name")] string LastName,
    [property: JsonPropertyName("username")]string Username,
    [property: JsonPropertyName("language_code")]string LanguageCode,
    [property: JsonPropertyName("is_premium")]bool IsPremium,
    [property: JsonPropertyName("added_to_attachment_menu")]bool AddedToAttachmentMenu,
    [property: JsonPropertyName("allows_write_to_pm")]bool AllowsWriteToPm,
    [property: JsonPropertyName("photo_url")] string PhotoUrl);