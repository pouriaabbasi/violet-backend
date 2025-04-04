using Microsoft.AspNetCore.Mvc;
using violet.backend.Controllers.Common;
using violet.backend.Models.Profile;
using violet.backend.Services.Common;
using violet.backend.Services.Contracts;

namespace violet.backend.Controllers
{
    public class ProfileController(
        ICurrentUserService currentUser,
        IUserBaseService userBaseService,
        UserServiceFactory factory) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var service = factory.CreateUserService(currentUser.User.Gender);
                var result = await service.GetProfile();
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
                var gender = currentUser.User.Gender;
                if (gender == null)
                {
                    await userBaseService.ConvertUserToDerivedUser(currentUser.User.Id, request.Gender);
                    gender = request.Gender;
                }

                var service = factory.CreateUserService(gender);
                var result = await service.UpdateProfile(request);
                return SuccessResult(result);
            }
            catch (Exception e)
            {
                return ErrorResult(e);
            }
        }
    }
}
