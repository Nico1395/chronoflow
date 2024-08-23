namespace ChronoFlow.Client.Common.MainData.Results;

public sealed record MainDataGetNewResult<TViewModel>(MainDataGetNewResultCode Code, TViewModel? Item, string? Message = null)
    where TViewModel : class;
