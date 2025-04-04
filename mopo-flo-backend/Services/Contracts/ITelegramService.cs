namespace violet.backend.Services.Contracts;

public interface ITelegramService
{
    bool ValidateTelegramData(string telegramData);
}