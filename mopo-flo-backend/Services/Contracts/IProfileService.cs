using violet.backend.Models.Profile;

namespace violet.backend.Services.Contracts;

public interface IProfileService
{
    Task<bool> HasProfile();
    Task<ProfileDto> GetProfile();
    Task<bool> UpdateProfile(UpdateProfileRequest request);
}