namespace mopo_flo_backend.Models.Auth;

public record LoginRequest(string TelegramData, TelegramUserModel UserModel);