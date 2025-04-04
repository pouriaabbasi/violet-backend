using violet.backend.Entities;
using violet.backend.Enums;
using violet.backend.Infrastructures.Repository;
using violet.backend.Models.Profile;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class MaleUserService(
    ICurrentUserService currentUser,
    IMaleUserRepository repository) : IUserService
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
            user.MaleProfile.Name,
            user.MaleProfile.BirthYear,
            user.MaleProfile.Height,
            user.MaleProfile.Weigh,
            default,
            default,
            GenderType.Male);
    }

    private Task<MaleUser> GetUser()
        => repository.GetUserFromUserId(currentUser.User.Id);
}