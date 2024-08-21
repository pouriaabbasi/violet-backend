using mopo_flo_backend.Models.Auth;
using mopo_flo_backend.Models.Common;

namespace mopo_flo_backend.Services.Contracts;

public interface IAuthService
{
    Task<string> Login(LoginRequest request);
}