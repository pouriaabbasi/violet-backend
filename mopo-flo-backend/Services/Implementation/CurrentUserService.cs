using violet.backend.Models.Auth;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class CurrentUserService : ICurrentUserService
{

    public CurrentUserModel User { get; private set; }

    public void Initialize(CurrentUserModel user)
    {
        User = user;
    }
}