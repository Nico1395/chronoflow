using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.Common.MainData.Persistence;

public interface IMainDataViewModelService<TViewModel>
    where TViewModel : class
{
    public Task<Result<List<TViewModel>>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<TViewModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Result> AddAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
    public Task<Result> UpdateAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
    public Task<Result> DeleteAsync(TViewModel viewModel, CancellationToken cancellationToken = default);
}
