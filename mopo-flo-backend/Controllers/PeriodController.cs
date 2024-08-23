using Microsoft.AspNetCore.Mvc;
using mopo_flo_backend.Controllers.Common;
using mopo_flo_backend.Models.Period;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Controllers;

public class PeriodController(IPeriodService periodService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetLastPeriod()
    {
        try
        {
            var result = await periodService.GetLastPeriod();
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPeriod(AddPeriodRequest request)
    {
        try
        {
            var result = periodService.AddPeriod(request);
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }
}