namespace ChronoFlow.Client.Common.MainData.Results;

public enum MainDataUpdateResultCode
{
    Success,
    Error,
    AlreadyExists,
    NotFound,
    MissingPermissions,
    ValidationErrors,
}
