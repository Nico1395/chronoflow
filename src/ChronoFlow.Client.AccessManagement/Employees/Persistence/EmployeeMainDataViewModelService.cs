using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.Common.Http;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.AccessManagement.Employees.Persistence;

internal sealed class EmployeeMainDataViewModelService(IServerHttpRequestService _httpRequestService) : IMainDataViewModelService<EmployeeViewModel>
{
    public Task<Result<List<EmployeeViewModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _httpRequestService.GetAsync<List<EmployeeDto>, List<EmployeeViewModel>>("api/access-management/employees/get-all", cancellationToken);
    }

    public Task<Result<EmployeeViewModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _httpRequestService.GetAsync<EmployeeDto, EmployeeViewModel>("api/access-management/employees/get-by-id", cancellationToken, ("employeeId", id));
    }

    public Task<Result<EmployeeViewModel>> GetNewAsync(CancellationToken cancellationToken = default)
    {
        return _httpRequestService.GetAsync<EmployeeDto, EmployeeViewModel>("api/access-management/employees/get-new", cancellationToken);
    }

    public Task<Result> AddAsync(EmployeeViewModel viewModel, CancellationToken cancellationToken = default)
    {
        return _httpRequestService.PostAsync<EmployeeDto, EmployeeViewModel>("api/access-management/employees/add", viewModel, cancellationToken);
    }

    public Task<Result> UpdateAsync(EmployeeViewModel viewModel, CancellationToken cancellationToken = default)
    {
        return _httpRequestService.PatchAsync<EmployeeDto, EmployeeViewModel>("api/access-management/employees/update", viewModel, cancellationToken);
    }

    public Task<Result> DeleteAsync(EmployeeViewModel viewModel, CancellationToken cancellationToken = default)
    {
        return _httpRequestService.DeleteAsync("api/access-management/employees/delete", cancellationToken, ("employeeId", viewModel.Id));
    }
}
