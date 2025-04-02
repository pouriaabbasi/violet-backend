using violet.backend.Models.Auth;

namespace violet.backend.Services.Contracts;

public interface IAuthService
{
    Task<string> LoginFromTelegram(TelegramLoginRequest request);
}