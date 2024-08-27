using ChronoFlow.Client.Common.Validation;

namespace ChronoFlow.Client.Common.MainData.Results;

public sealed record MainDataUpdateResult(
    MainDataUpdateResultCode Code,
    string? Message = null,
    List<ValidationError>? ValidationErrors = null);
