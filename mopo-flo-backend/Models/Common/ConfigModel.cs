namespace violet.backend.Models.Common;

public class ConfigModel
{
    public JwtOptionModel Jwt { get; set; }
    public TelegramBotOptionModel TelegramBot { get; set; }
    public ConnectionStringsOptionModel ConnectionStrings { get; set; }
}

public class ConnectionStringsOptionModel
{
    public string AppConnection { get; set; }
}

public class JwtOptionModel
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}

public class TelegramBotOptionModel
{
    public string BotToken { get; set; }
}