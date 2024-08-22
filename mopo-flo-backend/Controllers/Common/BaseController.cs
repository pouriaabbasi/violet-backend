using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mopo_flo_backend.Models.Common;

namespace mopo_flo_backend.Controllers.Common;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class BaseController : ControllerBase
{
    protected IActionResult SuccessResult<T>(T data, string message = "", int code = 200)
    {
        var result = new ApiResponse<T>(message, code, true, data);
        return Ok(result);
    }

    protected IActionResult ErrorResult(Exception exp)
    {
        var result = new ApiResponse<object>(exp.Message, 500, false, null);
        return Ok(result);
    }
}