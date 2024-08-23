using ChronoFlow.Client.Common.MainData.Results;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public interface IMainDataListService<TViewModel>
    where TViewModel : class
{
    public Task<MainDataGetAllResult<TViewModel>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<MainDataDeleteResult> DeleteAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
}
