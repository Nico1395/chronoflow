namespace ChronoFlow.Client.Common.MainData.Results;

public sealed record MainDataGetAllResult<TViewModel>(MainDataGetAllResultCode Code, List<TViewModel>? Items, string? Message = null)
    where TViewModel : class;
