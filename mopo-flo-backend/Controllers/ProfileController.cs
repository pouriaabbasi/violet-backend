using Microsoft.AspNetCore.Mvc;
using violet.backend.Controllers.Common;
using violet.backend.Models.Profile;
using violet.backend.Services.Contracts;

namespace violet.backend.Controllers
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
