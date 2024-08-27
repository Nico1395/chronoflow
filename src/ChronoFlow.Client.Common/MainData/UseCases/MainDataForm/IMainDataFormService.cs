using ChronoFlow.Client.Common.MainData.Entities;
using ChronoFlow.Client.Common.MainData.Results;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;

public interface IMainDataFormService<TViewModel>
    where TViewModel : class, IMainDataViewModel
{
    public Task<MainDataGetByIdResult<TViewModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<MainDataGetNewResult<TViewModel>> GetNewAsync(CancellationToken cancellationToken = default);
    public Task<MainDataAddResult> AddAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
    public Task<MainDataUpdateResult> UpdateAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
}
