using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Http;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.AccessManagement.Roles.Persistence;

internal sealed class RoleMainDataViewModelService(IServerHttpRequestService _httpRequestService) : IMainDataViewModelService<RoleViewModel>
{
    public Task<Result<List<RoleViewModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _httpRequestService.GetAsync<List<RoleDto>, List<RoleViewModel>>("api/access-management/roles/get-all", cancellationToken);
    }

    public Task<Result<RoleViewModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _httpRequestService.GetAsync<RoleDto, RoleViewModel>("api/access-management/roles/get-by-id", cancellationToken, ("roleId", id));
    }

    public Task<Result<RoleViewModel>> GetNewAsync(CancellationToken cancellationToken = default)
    {
        return _httpRequestService.GetAsync<RoleDto, RoleViewModel>("api/access-management/roles/get-new", cancellationToken);
    }

    public Task<Result> AddAsync(RoleViewModel viewModel, CancellationToken cancellationToken = default)
    {
        return _httpRequestService.PostAsync<RoleDto, RoleViewModel>("api/access-management/roles/add", viewModel, cancellationToken);
    }

    public Task<Result> UpdateAsync(RoleViewModel viewModel, CancellationToken cancellationToken = default)
    {
        return _httpRequestService.PatchAsync<RoleDto, RoleViewModel>("api/access-management/roles/update", viewModel, cancellationToken);
    }

    public Task<Result> DeleteAsync(RoleViewModel viewModel, CancellationToken cancellationToken = default)
    {
        return _httpRequestService.DeleteAsync("api/access-management/roles/delete", cancellationToken, ("roleId", viewModel.Id));
    }
}
