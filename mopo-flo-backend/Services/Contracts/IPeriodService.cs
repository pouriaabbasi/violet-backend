using mopo_flo_backend.Models.Common;
using mopo_flo_backend.Models.Period;

namespace mopo_flo_backend.Services.Contracts;

public interface IPeriodService
{
    Task<PeriodCycleInformationModel> GetPeriodCycleInformation();
    Task<bool> AddPeriod(AddPeriodRequest request);
    Task<bool> AddEndOfBleeding(AddEndOfBleedingRequest request);
    Task<TableResponse<PeriodHistoryModel>> GetPeriodHistory(TableRequest request);
    Task<PeriodLogInfoModel> GetPeriodLog(long periodLogId);
    Task<bool> UpdatePeriodLog(long periodLogId, UpdatePeriodLogRequest request);
    Task<bool> DeletePeriod(long periodLogId);
}