using ChronoFlow.Client.Common.MainData.Results;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;

public interface IMainDataFormService<TViewModel>
    where TViewModel : class
{
    public Task<MainDataGetNewResult<TViewModel>> GetNewAsync(CancellationToken cancellationToken = default);
    public Task<MainDataGetByIdResult<TViewModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<MainDataAddResult<TViewModel>> AddAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
    public Task<MainDataUpdateResult<TViewModel>> UpdateAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
}
