using mopo_flo_backend.Models.Period;

namespace mopo_flo_backend.Services.Contracts;

public interface IPeriodService
{
    Task<PeriodCycleInformationModel> GetPeriodCycleInformation();
    Task<bool> AddPeriod(AddPeriodRequest request);
    Task<bool> AddEndOfBleeding(AddEndOfBleedingRequest request);
}