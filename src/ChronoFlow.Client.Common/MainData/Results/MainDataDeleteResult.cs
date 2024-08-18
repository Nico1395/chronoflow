namespace ChronoFlow.Client.Common.MainData.Results;

public sealed record MainDataDeleteResult<TViewModel>(MainDataDeleteResultCode Code, string? Message = null)
    where TViewModel : class;
