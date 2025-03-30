using violet.backend.Models.Profile;

namespace violet.backend.Services.Contracts;

public interface IProfileService
{
    Task<bool> HasProfile();
    Task<ProfileModel> GetProfile();
    Task<bool> UpdateProfile(UpdateProfileRequest request);
}