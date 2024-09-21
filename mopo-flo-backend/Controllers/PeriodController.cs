using Microsoft.AspNetCore.Mvc;
using mopo_flo_backend.Controllers.Common;
using mopo_flo_backend.Models.Common;
using mopo_flo_backend.Models.Period;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Controllers;

public class PeriodController(IPeriodService periodService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetPeriodCycleInformation()
    {
        try
        {
            var result = await periodService.GetPeriodCycleInformation();
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }

    [HttpGet("{periodLogId}")]
    public async Task<IActionResult> GetPeriodLog(long periodLogId)
    {
        try
        {
            var result = await periodService.GetPeriodLog(periodLogId);
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
            var result = await periodService.AddPeriod(request);
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }

    [HttpPut]
    public async Task<IActionResult> AddEndOfBleeding(AddEndOfBleedingRequest request)
    {
        try
        {
            var result = await periodService.AddEndOfBleeding(request);
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }

    [HttpPost]
    public async Task<IActionResult> GetPeriodHistory(TableRequest request)
    {
        try
        {
            var result = await periodService.GetPeriodHistory(request);
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }

    [HttpPut("{periodLogId}")]
    public async Task<IActionResult> UpdatePeriod(long periodLogId, UpdatePeriodLogRequest request)
    {
        try
        {
            var result = await periodService.UpdatePeriodLog(periodLogId, request);
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePeriod(long periodLogId)
    {
        try
        {
            var result = await periodService.DeletePeriod(periodLogId);
            return SuccessResult(result);
        }
        catch (Exception e)
        {
            return ErrorResult(e);
        }
    }
}