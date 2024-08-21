namespace mopo_flo_backend.Models.Common;

public record ConfigModel(
    JwtOptionModel Jwt,
    TelegramBotOptionModel TelegramBot,
    ConnectionStringsOptionModel ConnectionStrings);

public record ConnectionStringsOptionModel(string AppConnection);
public record JwtOptionModel(string Issuer, string Audience, string SecretKey);
public record TelegramBotOptionModel(string BotToken);