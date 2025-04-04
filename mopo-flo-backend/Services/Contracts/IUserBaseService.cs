using violet.backend.Enums;

namespace violet.backend.Services.Contracts;

public interface IUserBaseService
{
    Task ConvertUserToDerivedUser(Guid userId, GenderType gender);
}