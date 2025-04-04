using violet.backend.Enums;
using violet.backend.Infrastructures.Repository;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class UserBaseService(IUserRepository repository) : IUserBaseService
{
    public Task ConvertUserToDerivedUser(Guid userId, GenderType gender)
        => repository.ConvertUserToDerivedUser(userId, gender);
}