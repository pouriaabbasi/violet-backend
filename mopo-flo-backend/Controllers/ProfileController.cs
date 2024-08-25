using Microsoft.AspNetCore.Mvc;
using mopo_flo_backend.Controllers.Common;
using mopo_flo_backend.Models.Profile;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Controllers
{
    public class ProfileController(IProfileService profileService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> HasProfile()
        {
            try
            {
                var result = await profileService.HasProfile();
                return SuccessResult(result);
            }
            catch (Exception e)
            {
                return ErrorResult(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var result = await profileService.GetProfile();
                return SuccessResult(result);
            }
            catch (Exception e)
            {
                return ErrorResult(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
        {
            try
            {
                var result = await profileService.UpdateProfile(request);
                return SuccessResult(result);
            }
            catch (Exception e)
            {
                return ErrorResult(e);
            }
        }
    }
}
