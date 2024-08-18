using ChronoFlow.Client.Common.Validation;

namespace ChronoFlow.Client.Common.MainData.Results;

public sealed record MainDataAddResult<TViewModel>(MainDataAddResultCode Code, TViewModel? Item, string? Message = null, List<ValidationError>? ValidationErrors = null)
    where TViewModel : class;
