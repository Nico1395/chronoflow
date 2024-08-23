using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.AccessManagement.Employees.Persistence;

internal sealed class EmployeeMainDataViewModelService : IMainDataViewModelService<EmployeeViewModel>
{
    public Task<Result<List<EmployeeViewModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<EmployeeViewModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> AddAsync(EmployeeViewModel viewModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(EmployeeViewModel viewModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(EmployeeViewModel viewModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
