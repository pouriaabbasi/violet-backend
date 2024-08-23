using mopo_flo_backend.Models.Auth;

namespace mopo_flo_backend.Services.Contracts;

public interface ICurrentUserService
{
    public CurrentUserModel User { get;}
    void Initialize(CurrentUserModel user);
}