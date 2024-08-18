using ChronoFlow.Client.Common.MainData.Results;

namespace ChronoFlow.Client.Common.MainData;

public interface IMainDataListService<TViewModel>
    where TViewModel : class
{
    public Task<MainDataGetAllResult<TViewModel>> GetAllAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
    public Task<MainDataDeleteResult<TViewModel>> DeleteAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
}
