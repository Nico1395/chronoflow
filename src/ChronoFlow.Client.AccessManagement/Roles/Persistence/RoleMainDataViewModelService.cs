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

    public Task<Result<RoleViewModel?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> AddAsync(RoleViewModel viewModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(RoleViewModel viewModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(RoleViewModel viewModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
