using Microsoft.EntityFrameworkCore;
using mopo_flo_backend.Entities;
using mopo_flo_backend.Enums;
using mopo_flo_backend.Infrastructures;
using mopo_flo_backend.Models.Period;
using mopo_flo_backend.Models.Profile;
using mopo_flo_backend.Services.Common;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Services.Implementation;

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

        var entity = new PeriodLog
        {
            StartDayOfPeriod = request.StartDayOfPeriod.ToGregorianDate(),
            TelegramUserId = currentUserService.User.Id
        };

        await appDbContext.PeriodLogs.AddAsync(entity);
        await appDbContext.SaveChangesAsync();

        return true;
    }

    public async  Task<bool> AddEndOfBleeding(AddEndOfBleedingRequest request)
    {
        var lastPeriodLog = await GetLastPeriodInfo();
        lastPeriodLog.EndDayOfBleeding = request.EndDayOfBleeding.ToGregorianDate();

        await appDbContext.SaveChangesAsync();

        return true;
    }

    private async Task CheckAddPeriodRequest(AddPeriodRequest request)
    {
        var isExist = await appDbContext.PeriodLogs.AnyAsync(x =>
            x.TelegramUserId == currentUserService.User.Id &&
            x.StartDayOfPeriod == request.StartDayOfPeriod.ToGregorianDate());
        if (isExist)
            throw new Exception("تاریخ انتخاب شده تکراری است و قبلا این تاریخ انتخاب شده است");
    }

    private static PeriodCycleInformationModel CalculatePeriodCycleInformation(PeriodLog lastPeriodInfo, PeriodStatisticsModel userPeriodStatistics)
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

    private static int GetDayOfCycle(PeriodLog lastPeriodInfo)
    {
        return (DateTime.Now - lastPeriodInfo.StartDayOfPeriod).Days + 1;
    }

    private static ChanceOfPregnancyType CalculateChanceOfPregnancy(PeriodLog lastPeriodInfo, PeriodCycleStateType currentStatus, PeriodStatisticsModel userPeriodStatistics)
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

    private static DateTime CalculateEndDayOfCurrentStatus(PeriodCycleStateType currentStatus, PeriodLog lastPeriodInfo, PeriodStatisticsModel userPeriodStatistics)
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

    private static PeriodCycleStateType CalculateCurrentStatus(PeriodLog lastPeriodInfo, PeriodStatisticsModel userPeriodStatistics)
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

    private async Task<PeriodStatisticsModel> GetPeriodStatistics(ProfileModel profileInfo)
    {
        var logCount = appDbContext.PeriodLogs.Count(x => x.TelegramUserId == currentUserService.User.Id);
        if (logCount < 3)
        {
            return profileInfo.IsNewInPeriod
                ? new PeriodStatisticsModel(ProfileModel.DefaultPeriodCycleDuration, ProfileModel.DefaultBleedingDuration)
                : new PeriodStatisticsModel(profileInfo.PeriodCycleDuration, profileInfo.BleedingDuration);
        }

        var periodsInfo = await appDbContext.PeriodLogs
            .Where(x => x.TelegramUserId == currentUserService.User.Id)
            .Select(x => new PeriodLogInfo(x.StartDayOfPeriod, x.EndDayOfBleeding))
            .OrderBy(x => x.StartDayOfPeriod)
            .ToListAsync();

        var averagePeriodCycleDuration = CalculateAveragePeriodCycleDuration(periodsInfo);
        var averageBleedingDuration = CalculateBleedingDuration(periodsInfo);

        return new PeriodStatisticsModel(averagePeriodCycleDuration, averageBleedingDuration);
    }

    private static int CalculateBleedingDuration(List<PeriodLogInfo> periodsInfo)
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

        var average = (double)duration / count;

        return (int)Math.Ceiling(average);
    }

    private static int CalculateAveragePeriodCycleDuration(List<PeriodLogInfo> periodsInfo)
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

    private async Task<PeriodLog> GetLastPeriodInfo()
    {
        return
            await appDbContext.PeriodLogs
                .Where(x => x.TelegramUserId == currentUserService.User.Id)
                .OrderByDescending(x => x.StartDayOfPeriod)
                .FirstOrDefaultAsync();
    }
}