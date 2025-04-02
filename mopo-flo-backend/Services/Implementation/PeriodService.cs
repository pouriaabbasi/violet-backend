using Microsoft.EntityFrameworkCore;
using violet.backend.Entities;
using violet.backend.Enums;
using violet.backend.Infrastructures;
using violet.backend.Models.Common;
using violet.backend.Models.Period;
using violet.backend.Models.Profile;
using violet.backend.Services.Common;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class PeriodService(
    AppDbContext appDbContext,
    IProfileService profileService,
    ICurrentUserService currentUserService) : IPeriodService
{
    public async Task<PeriodCycleInformationModel> GetPeriodCycleInformation()
    {
        var lastPeriodInfo = await GetLastPeriodInfo();
        if (lastPeriodInfo == null) return null;

        var profileInfo = await profileService.GetProfile();

        var userPeriodStatistics = await GetPeriodStatistics(profileInfo);

        return CalculatePeriodCycleInformation(lastPeriodInfo, userPeriodStatistics);
    }

    public async Task<bool> AddPeriod(AddPeriodRequest request)
    {
        await CheckAddPeriodRequest(request);

        var entity = new Period
        {
            StartDayOfPeriod = request.StartDayOfPeriod.ToGregorianDate()!.Value
        };

        //await appDbContext.PeriodLogs.AddAsync(entity);
        await appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddEndOfBleeding(AddEndOfBleedingRequest request)
    {
        var lastPeriodLog = await GetLastPeriodInfo();
        lastPeriodLog.EndDayOfBleeding = request.EndDayOfBleeding.ToGregorianDate();

        await appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<TableResponse<PeriodHistoryModel>> GetPeriodHistory(TableRequest request)
    {
        //var result = await appDbContext.PeriodLogs
        //    //.Where(x => x.TelegramUserId == currentUserService.User.Id)
        //    .OrderByDescending(x => x.StartDayOfPeriod)
        //    .Skip((request.Page - 1) * request.PageSize)
        //    .Take(request.PageSize)
        //    .Select(x => new PeriodHistoryModel(x.Id, x.StartDayOfPeriod, x.EndDayOfBleeding))
        //    .ToListAsync();

        //ProcessPeriodHistory(result, request.Page, request.PageSize);

        //var totalCount = 0;//appDbContext.PeriodLogs.Count(x => x.TelegramUserId == currentUserService.User.Id);

        //return new TableResponse<PeriodHistoryModel>(result, request.Page, totalCount);

        return new TableResponse<PeriodHistoryModel>([], 0, 0);
    }

    public async Task<PeriodLogInfoModel> GetPeriodLog(Guid periodLogId)
    {
        //var periodLog = await appDbContext.PeriodLogs.FirstOrDefaultAsync(x => x.Id == periodLogId);

        //if (periodLog == null)
        //    throw new Exception("شناسه مورد نظر یافت نشد");

        //return new PeriodLogInfoModel(periodLog.Id, periodLog.StartDayOfPeriod, periodLog.EndDayOfBleeding);
        return new PeriodLogInfoModel(default, default, null);
    }

    public async Task<bool> UpdatePeriodLog(Guid periodLogId, UpdatePeriodLogRequest request)
    {
        await CheckAddPeriodRequest(periodLogId, request);

        //var periodLog = await appDbContext.PeriodLogs
        //    .FirstOrDefaultAsync(x => x.Id == periodLogId);

        //periodLog.StartDayOfPeriod = request.StartDayOfPeriod.ToGregorianDate()!.Value;
        //periodLog.EndDayOfBleeding = request.EndDayOfBleeding.ToGregorianDate();

        //await appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePeriod(Guid periodLogId)
    {
        //var periodLog = await appDbContext.PeriodLogs.FirstOrDefaultAsync(x => x.Id == periodLogId);

        //if (periodLog == null)
        //    throw new Exception("شناسه مورد نظر یافت نشد");

        //appDbContext.PeriodLogs.Remove(periodLog);
        //await appDbContext.SaveChangesAsync();

        return true;
    }

    private static void ProcessPeriodHistory(List<PeriodHistoryModel> periodHistories, int page, int pageSize)
    {
        for (var i = periodHistories.Count - 1; i >= 0; i--)
        {
            var currentPeriod = periodHistories[i];
            currentPeriod.Row = (page - 1) * pageSize + i + 1;
            if (i == 0) continue;

            var nextPeriod = periodHistories[i - 1];
            currentPeriod.StartDateOfNextPeriod = nextPeriod.StartDate;
        }
    }

    private async Task CheckAddPeriodRequest(AddPeriodRequest request)
    {
        //if (string.IsNullOrWhiteSpace(request.StartDayOfPeriod))
        //    throw new Exception("تاریخ شروع را وارد نمایید");

        //var isExist = await appDbContext.PeriodLogs.AnyAsync(x =>
        //    x.StartDayOfPeriod == request.StartDayOfPeriod.ToGregorianDate());
        //if (isExist)
            throw new Exception("تاریخ انتخاب شده تکراری است و قبلا این تاریخ انتخاب شده است");
    }

    private async Task CheckAddPeriodRequest(Guid periodLogId, UpdatePeriodLogRequest request)
    {
        //if (string.IsNullOrWhiteSpace(request.StartDayOfPeriod))
        //    throw new Exception("تاریخ شروع را وارد نمایید");

        //var isExist = await appDbContext.PeriodLogs.AnyAsync(x =>
        //    x.Id != periodLogId &&
        //    x.StartDayOfPeriod == request.StartDayOfPeriod.ToGregorianDate());
        //if (isExist)
            throw new Exception("تاریخ انتخاب شده تکراری است و قبلا این تاریخ انتخاب شده است");
    }

    private static PeriodCycleInformationModel CalculatePeriodCycleInformation(Period lastPeriodInfo, PeriodStatisticsModel userPeriodStatistics)
    {
        var dayOfCycle = GetDayOfCycle(lastPeriodInfo);
        var currentStatus = CalculateCurrentStatus(lastPeriodInfo, userPeriodStatistics);
        var endDayOfCurrentStatus = CalculateEndDayOfCurrentStatus(currentStatus, lastPeriodInfo, userPeriodStatistics);
        var nextStatus = CalculateNextStatus(currentStatus);
        var startDayOfNextStatus = CalculateStartDayOfNextStatus(endDayOfCurrentStatus);
        var chanceOfPregnancy = CalculateChanceOfPregnancy(lastPeriodInfo, currentStatus, userPeriodStatistics);
        return new PeriodCycleInformationModel(
            lastPeriodInfo.Id,
            dayOfCycle,
            lastPeriodInfo.StartDayOfPeriod,
            currentStatus,
            endDayOfCurrentStatus,
            nextStatus,
            startDayOfNextStatus,
            chanceOfPregnancy);
    }

    private static int GetDayOfCycle(Period lastPeriodInfo)
    {
        return (DateTime.Now - lastPeriodInfo.StartDayOfPeriod).Days + 1;
    }

    private static ChanceOfPregnancyType CalculateChanceOfPregnancy(Period lastPeriodInfo, PeriodCycleStateType currentStatus, PeriodStatisticsModel userPeriodStatistics)
    {
        var dayOfCycle = GetDayOfCycle(lastPeriodInfo);
        var ovulationDay = GetDayOfOvulation(userPeriodStatistics);

        if (currentStatus == PeriodCycleStateType.Menstrual)
            return ChanceOfPregnancyType.Low;

        if (currentStatus == PeriodCycleStateType.Follicular)
        {
            var remainToOvulationDay = ovulationDay - dayOfCycle;
            return remainToOvulationDay <= 5 ? ChanceOfPregnancyType.High : ChanceOfPregnancyType.Low;
        }

        if (currentStatus == PeriodCycleStateType.Ovulation)
            return ChanceOfPregnancyType.High;


        var remainToNextCycle = dayOfCycle - ovulationDay;
        return remainToNextCycle <= 3 ? ChanceOfPregnancyType.High : ChanceOfPregnancyType.Low;
    }

    private static int GetDayOfOvulation(PeriodStatisticsModel userPeriodStatistics)
    {
        return userPeriodStatistics.AveragePeriodCycleDuration / 2;
    }

    private static DateTime CalculateStartDayOfNextStatus(DateTime endDayOfCurrentStatus)
    {
        return endDayOfCurrentStatus.AddDays(1);
    }

    private static PeriodCycleStateType CalculateNextStatus(PeriodCycleStateType currentStatus)
    {
        return currentStatus switch
        {
            PeriodCycleStateType.Menstrual => PeriodCycleStateType.Follicular,
            PeriodCycleStateType.Follicular => PeriodCycleStateType.Ovulation,
            PeriodCycleStateType.Ovulation => PeriodCycleStateType.Follicular,
            PeriodCycleStateType.Luteal => PeriodCycleStateType.Menstrual,
            _ => PeriodCycleStateType.Menstrual
        };
    }

    private static DateTime CalculateEndDayOfCurrentStatus(PeriodCycleStateType currentStatus, Period lastPeriodInfo, PeriodStatisticsModel userPeriodStatistics)
    {
        var dayOfCycle = GetDayOfCycle(lastPeriodInfo);
        var ovulationDay = GetDayOfOvulation(userPeriodStatistics);

        if (currentStatus == PeriodCycleStateType.Menstrual)
        {
            var remainOfBleeding = userPeriodStatistics.AverageBleedingDuration - dayOfCycle;
            if (remainOfBleeding < 0)
                return DateTime.Now;

            return DateTime.Now.AddDays(remainOfBleeding);
        }

        if (currentStatus == PeriodCycleStateType.Follicular)
        {
            var remainToOvulationDay = ovulationDay - dayOfCycle;
            return DateTime.Now.AddDays(remainToOvulationDay);
        }

        if (currentStatus == PeriodCycleStateType.Ovulation)
            return DateTime.Now;

        return DateTime.Now.AddDays(userPeriodStatistics.AveragePeriodCycleDuration - dayOfCycle);
    }

    private static PeriodCycleStateType CalculateCurrentStatus(Period lastPeriodInfo, PeriodStatisticsModel userPeriodStatistics)
    {
        var dayOfCycle = GetDayOfCycle(lastPeriodInfo);
        var ovulationDay = GetDayOfOvulation(userPeriodStatistics);

        if (lastPeriodInfo.EndDayOfBleeding == null)
            return PeriodCycleStateType.Menstrual;

        if (dayOfCycle < ovulationDay)
            return PeriodCycleStateType.Follicular;

        if (dayOfCycle == ovulationDay)
            return PeriodCycleStateType.Ovulation;

        return PeriodCycleStateType.Luteal;
    }

    private async Task<PeriodStatisticsModel> GetPeriodStatistics(ProfileDto profileInfo)
    {
        //var logCount = 0;//appDbContext.PeriodLogs.Count(x => x.TelegramUserId == currentUserService.User.Id);
        //if (logCount < 3)
        //{
        //    return profileInfo.IsNewInPeriod
        //        ? new PeriodStatisticsModel(ProfileModel.DefaultPeriodCycleDuration, ProfileModel.DefaultBleedingDuration)
        //        : new PeriodStatisticsModel(profileInfo.PeriodCycleDuration, profileInfo.BleedingDuration);
        //}

        //var periodsInfo = await appDbContext.PeriodLogs
        //    //.Where(x => x.TelegramUserId == currentUserService.User.Id)
        //    .OrderBy(x => x.StartDayOfPeriod)
        //    .Select(x => new PeriodLogInfoModel(x.Id, x.StartDayOfPeriod, x.EndDayOfBleeding))
        //    .ToListAsync();

        //var averagePeriodCycleDuration = CalculateAveragePeriodCycleDuration(periodsInfo);
        //var averageBleedingDuration = CalculateBleedingDuration(periodsInfo);

        //return new PeriodStatisticsModel(
        //    averagePeriodCycleDuration,
        //    averageBleedingDuration == 0 ? profileInfo.BleedingDuration : averageBleedingDuration);

        return new PeriodStatisticsModel(default, default);
    }

    private static int CalculateBleedingDuration(List<PeriodLogInfoModel> periodsInfo)
    {
        var duration = 0;
        var count = 0;
        foreach (var period in periodsInfo)
        {
            if (period.EndDayOfBleeding == null)
                break;

            duration += (period.EndDayOfBleeding.Value - period.StartDayOfPeriod).Days;
            count++;
        }

        var average = duration == 0
            ? 0
            : (double)duration / count;

        return (int)Math.Ceiling(average);
    }

    private static int CalculateAveragePeriodCycleDuration(List<PeriodLogInfoModel> periodsInfo)
    {
        var count = 0;
        var duration = 0;
        DateTime? lastStartDate = null;
        foreach (var info in periodsInfo)
        {
            if (lastStartDate == null)
            {
                lastStartDate = info.StartDayOfPeriod;
                continue;
            }

            duration += (info.StartDayOfPeriod - lastStartDate.Value).Days;
            lastStartDate = info.StartDayOfPeriod;
            count++;
        }

        var average = (double)duration / count;

        return (int)Math.Ceiling(average);
    }

    private async Task<Period> GetLastPeriodInfo()
    {
        //return
        //    await appDbContext.PeriodLogs
        //        //.Where(x => x.TelegramUserId == currentUserService.User.Id)
        //        .OrderByDescending(x => x.StartDayOfPeriod)
        //        .FirstOrDefaultAsync();

        return new Period();
    }
}