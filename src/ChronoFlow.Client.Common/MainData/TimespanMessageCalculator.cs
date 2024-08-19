using ChronoFlow.Client.Common.Localization;

namespace ChronoFlow.Client.Common.MainData;

internal sealed class TimespanMessageCalculator(ILocalizer _localizer) : ITimespanMessageCalculator
{
    public string? GetCreatedMessage(DateTime timestampCreated)
    {
        return CalculateTimespanMessage(timestampCreated, "Created");
    }

    public string? GetEditedMessage(DateTime timestampEdited)
    {
        return CalculateTimespanMessage(timestampEdited, "Changed");
    }

    private string? CalculateTimespanMessage(DateTime timestamp, string prefix)
    {
        var differential = DateTime.UtcNow - timestamp;
        if (differential.TotalSeconds < 60)
        {
            return _localizer[$"{prefix}JustNow"];
        }
        else if (differential.TotalMinutes < 2)
        {
            return _localizer[$"{prefix}AMinuteAgo"];
        }
        else if (differential.TotalMinutes < 60)
        {
            return _localizer[$"{prefix}MinutesAgo", (int)differential.TotalMinutes];
        }
        else if (differential.TotalHours < 2)
        {
            return _localizer[$"{prefix}AnHourAgo"];
        }
        else if (differential.TotalHours < 24)
        {
            return _localizer[$"{prefix}HoursAgo", (int)differential.TotalHours];
        }
        else if (differential.TotalDays < 2)
        {
            return _localizer[$"{prefix}ADayAgo"];
        }
        else if (differential.TotalDays < 7)
        {
            return _localizer[$"{prefix}DaysAgo", (int)differential.TotalDays];
        }
        else if (differential.TotalDays < 14)
        {
            return _localizer[$"{prefix}AWeekAgo"];
        }
        else if (differential.TotalDays < 30)
        {
            return _localizer[$"{prefix}WeeksAgo", (int)(differential.TotalDays / 7)];
        }
        else if (differential.TotalDays < 60)
        {
            return _localizer[$"{prefix}AMonthAgo"];
        }
        else if (differential.TotalDays < 365)
        {
            return _localizer[$"{prefix}MonthsAgo", (int)(differential.TotalDays / 30)];
        }
        else if (differential.TotalDays < 730)
        {
            return _localizer[$"{prefix}AYearAgo"];
        }
        else
        {
            return _localizer[$"{prefix}YearsAgo", (int)(differential.TotalDays / 365)];
        }
    }
}