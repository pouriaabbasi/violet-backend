using mopo_flo_backend.Models.Period;

namespace mopo_flo_backend.Services.Contracts;

public interface IPeriodService
{
    Task<PeriodLogModel> GetLastPeriod();
    Task<PeriodLogModel> AddPeriod(AddPeriodRequest request);
}