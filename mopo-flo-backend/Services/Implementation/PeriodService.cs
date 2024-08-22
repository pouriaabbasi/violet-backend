using Microsoft.EntityFrameworkCore;
using mopo_flo_backend.Infrastructures;
using mopo_flo_backend.Models.Period;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Services.Implementation;

public class PeriodService(AppDbContext appDbContext) : IPeriodService
{
    public async Task<PeriodLogModel> GetLastPeriod()
    {
        var periodLogEntity =
            await appDbContext.PeriodLogs.OrderByDescending(x => x.StartDayOfPeriod).FirstOrDefaultAsync();

        return periodLogEntity == null
            ? null
            : new PeriodLogModel(periodLogEntity.Id, periodLogEntity.StartDayOfPeriod);
    }
}