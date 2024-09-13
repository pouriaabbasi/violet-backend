using mopo_flo_backend.Enums;
using mopo_flo_backend.Services.Common;

namespace mopo_flo_backend.Models.Period;

public record PeriodCycleInformationModel(
    long PeriodLogId,
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
            PeriodCycleStateType.Follicular => "فولیکولی",
            PeriodCycleStateType.Ovulation => "تخمک گذاری",
            PeriodCycleStateType.Luteal => "لوتئال",
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