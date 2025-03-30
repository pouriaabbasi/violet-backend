using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using violet.backend.Controllers.Common;
using violet.backend.Models.Auth;
using violet.backend.Services.Contracts;

namespace violet.backend.Controllers;

public class LoginController(IAuthService authService) : BaseController
{
    [HttpPost]
    [AllowAnonymous]
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