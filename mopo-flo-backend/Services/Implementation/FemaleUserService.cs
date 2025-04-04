using violet.backend.Entities;
using violet.backend.Enums;
using violet.backend.Infrastructures.Repository;
using violet.backend.Models.Profile;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class FemaleUserService(
    ICurrentUserService currentUser,
    IFemaleUserRepository repository) : IUserService
{
    public async Task<bool> UpdateProfile(UpdateProfileRequest request)
    {
        var user = await GetUser();

        await repository.UpdateProfile(user, request);

        return true;
    }

    public async Task<ProfileDto> GetProfile()
    {
        var user = await GetUser();

        return new ProfileDto(
            user.FemaleProfile.Name,
            user.FemaleProfile.BirthYear,
            user.FemaleProfile.Height,
            user.FemaleProfile.Weigh,
            user.FemaleProfile.PeriodCycleDuration,
            user.FemaleProfile.BleedingDuration,
            GenderType.Female);
    }

    private Task<FemaleUser> GetUser()
        => repository.GetUserFromUserId(currentUser.User.Id);
}