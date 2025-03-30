using violet.backend.Models.Auth;

namespace violet.backend.Services.Contracts;

public interface ICurrentUserService
{
    public CurrentUserModel User { get;}
    void Initialize(CurrentUserModel user);
}