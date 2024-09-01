using ChronoFlow.Client.AccessManagement.Permissions.Entities;
using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.AccessManagement.Permissions.Persistence;

internal interface IPermissionMainDataViewModelService
{
    public Task<Result<List<PermissionViewModel>>> GetAllAsync(CancellationToken cancellationToken = default);
}
