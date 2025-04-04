using violet.backend.Models.Profile;

namespace violet.backend.Services.Contracts;

public interface IUserService
{
    Task<bool> UpdateProfile(UpdateProfileRequest request);
    Task<ProfileDto> GetProfile();
}