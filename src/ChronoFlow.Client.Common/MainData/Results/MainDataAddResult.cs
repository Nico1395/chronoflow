using ChronoFlow.Client.Common.Validation;

namespace ChronoFlow.Client.Common.MainData.Results;

public sealed record MainDataAddResult(
    MainDataAddResultCode Code,
    string? Message = null,
    List<ValidationError>? ValidationErrors = null);
