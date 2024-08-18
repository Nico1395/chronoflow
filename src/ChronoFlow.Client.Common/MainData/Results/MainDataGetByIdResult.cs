namespace ChronoFlow.Client.Common.MainData.Results;

public sealed record MainDataGetByIdResult<TViewModel>(MainDataGetByIdResultCode Code, TViewModel? Item, string? Message = null)
    where TViewModel : class;
