using ChronoFlow.Client.AccessManagement.Permissions.Entities;
using ChronoFlow.Client.Common.Http;
using ChronoFlow.Shared.AccessManagement.Permissions;
using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.AccessManagement.Permissions.Persistence;

internal sealed class PermissionMainDataViewModelService(IServerHttpRequestService _httpRequestService) : IPermissionMainDataViewModelService
{
    public Task<Result<List<PermissionViewModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _httpRequestService.GetAsync<List<PermissionDto>, List<PermissionViewModel>>("api/access-management/permissions/get-all", cancellationToken);
    }
}
