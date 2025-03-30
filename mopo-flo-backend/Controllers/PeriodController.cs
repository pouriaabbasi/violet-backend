using Microsoft.AspNetCore.Mvc;
using violet.backend.Controllers.Common;
using violet.backend.Models.Common;
using violet.backend.Models.Period;
using violet.backend.Services.Contracts;

namespace violet.backend.Controllers;

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
    public async Task<IActionResult> GetPeriodLog(Guid periodLogId)
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
    public async Task<IActionResult> UpdatePeriod(Guid periodLogId, UpdatePeriodLogRequest request)
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

    [HttpDelete("{periodLogId}")]
    public async Task<IActionResult> DeletePeriod(Guid periodLogId)
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