﻿using Microsoft.EntityFrameworkCore;
using mopo_flo_backend.Entities;
using mopo_flo_backend.Infrastructures;
using mopo_flo_backend.Models.Period;
using mopo_flo_backend.Services.Common;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Services.Implementation;

public class PeriodService(
    AppDbContext appDbContext,
    ICurrentUserService currentUserService) : IPeriodService
{
    public async Task<PeriodLogModel> GetLastPeriod()
    {
        var periodLogEntity =
            await appDbContext.PeriodLogs
                .Where(x => x.TelegramUserId == currentUserService.User.Id)
                .OrderByDescending(x => x.StartDayOfPeriod)
                .FirstOrDefaultAsync();

        return periodLogEntity == null
            ? null
            : new PeriodLogModel(periodLogEntity.Id, periodLogEntity.StartDayOfPeriod);
    }

    public async Task<PeriodLogModel> AddPeriod(AddPeriodRequest request)
    {
        var entity = new PeriodLog
        {
            StartDayOfPeriod = request.StartDayOfPeriod.ToGregorianDate(),
            TelegramUserId = currentUserService.User.Id
        };

        await appDbContext.PeriodLogs.AddAsync(entity);
        await appDbContext.SaveChangesAsync();

        return new PeriodLogModel(entity.Id, entity.StartDayOfPeriod);
    }
}