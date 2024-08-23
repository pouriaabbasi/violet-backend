using mopo_flo_backend.Models.Auth;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Services.Implementation;

public class CurrentUserService : ICurrentUserService
{

    public CurrentUserModel User { get; private set; }

    public void Initialize(CurrentUserModel user)
    {
        User = user;
    }
}