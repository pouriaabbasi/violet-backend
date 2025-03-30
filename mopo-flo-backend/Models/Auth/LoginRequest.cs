namespace violet.backend.Models.Auth;

public record LoginRequest(string TelegramData, TelegramInfoDto UserDto);