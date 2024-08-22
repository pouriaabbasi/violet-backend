using Microsoft.AspNetCore.Mvc;
using mopo_flo_backend.Controllers.Common;
using mopo_flo_backend.Models.Auth;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Controllers;

public class LoginController(IAuthService authService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var result = await authService.Login(request);
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }
}