using violet.backend.Enums;
using violet.backend.Services.Contracts;
using violet.backend.Services.Implementation;

namespace violet.backend.Services.Common;

public class UserServiceFactory(IEnumerable<IUserService> services)
{
    public IUserService CreateUserService(GenderType? gender)
    {
        return gender switch
        {
            GenderType.Female => services.OfType<FemaleUserService>().FirstOrDefault(),
            GenderType.Male => services.OfType<MaleUserService>().FirstOrDefault(),
            _ => throw new InvalidOperationException("Invalid gender type")
        };
    }
}