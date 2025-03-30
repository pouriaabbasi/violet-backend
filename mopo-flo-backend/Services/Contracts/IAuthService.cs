using violet.backend.Models.Auth;

namespace violet.backend.Services.Contracts;

public interface IAuthService
{
    Task<string> Login(LoginRequest request);
}