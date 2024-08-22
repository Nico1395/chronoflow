using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.MainData.Results;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases;

internal sealed class RoleMainDataListService : IMainDataListService<RoleViewModel>
{
    public Task<MainDataDeleteResult> DeleteAsync(RoleViewModel viewModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<MainDataGetAllResult<RoleViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
