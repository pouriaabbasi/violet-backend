using violet.backend.Enums;
using violet.backend.Services.Common;

namespace violet.backend.Models.Period;

public record PeriodCycleInformationModel(
    Guid PeriodLogId,
    int DayOfCycle,
    DateTime StartDayOfPeriod,
    PeriodCycleStateType CurrentPeriodCycleState,
    DateTime EndDayOfCurrentState,
    PeriodCycleStateType NextPeriodCycleState,
    DateTime StartDayOfNextState,
    ChanceOfPregnancyType ChanceOfPregnancy
    )
{
    public string StartDayOfPeriodPersian => StartDayOfPeriod.ToJalaliDate();
    public string EndDayOfCurrentStatePersian => EndDayOfCurrentState.ToJalaliDate();
    public string StartDayOfNextStatePersian => StartDayOfNextState.ToJalaliDate();
    public string CurrentPeriodCycleStateName => ConvertStatusToFriendlyName(CurrentPeriodCycleState);
    public string NextPeriodCycleStateName => ConvertStatusToFriendlyName(NextPeriodCycleState);
    public string ChanceOfPregnancyName => ConvertStatusToFriendlyName(ChanceOfPregnancy);

    private static string ConvertStatusToFriendlyName(PeriodCycleStateType periodCycleState)
    {
        return periodCycleState switch
        {
            PeriodCycleStateType.Menstrual => "قاعدگی",
            PeriodCycleStateType.Follicular => "اتمام قاعدگی (قبل از تخمک گذاری)",
            PeriodCycleStateType.Ovulation => "تخمک گذاری",
            PeriodCycleStateType.Luteal => "اتمام تخمک گذاری",
            _ => string.Empty
        };
    }

    private static string ConvertStatusToFriendlyName(ChanceOfPregnancyType changePregnancy)
    {
        return changePregnancy switch
        {
            ChanceOfPregnancyType.Low => "کم",
            ChanceOfPregnancyType.High => "زیاد",
            _ => string.Empty
        };
    }
}