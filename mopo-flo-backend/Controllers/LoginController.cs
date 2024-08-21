using Microsoft.AspNetCore.Mvc;
using mopo_flo_backend.Models.Auth;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoginController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await authService.Login(request);
        return Ok(result);
    }
}