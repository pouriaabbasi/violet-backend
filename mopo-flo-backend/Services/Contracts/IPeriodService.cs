using violet.backend.Models.Common;
using violet.backend.Models.Period;

namespace violet.backend.Services.Contracts;

public interface IPeriodService
{
    Task<PeriodCycleInformationModel> GetPeriodCycleInformation();
    Task<bool> AddPeriod(AddPeriodRequest request);
    Task<bool> AddEndOfBleeding(AddEndOfBleedingRequest request);
    Task<TableResponse<PeriodHistoryModel>> GetPeriodHistory(TableRequest request);
    Task<PeriodLogInfoModel> GetPeriodLog(Guid periodLogId);
    Task<bool> UpdatePeriodLog(Guid periodLogId, UpdatePeriodLogRequest request);
    Task<bool> DeletePeriod(Guid periodLogId);
}