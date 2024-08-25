using mopo_flo_backend.Models.Profile;

namespace mopo_flo_backend.Services.Contracts;

public interface IProfileService
{
    Task<bool> HasProfile();
    Task<ProfileModel> GetProfile();
    Task<bool> UpdateProfile(UpdateProfileRequest request);
}