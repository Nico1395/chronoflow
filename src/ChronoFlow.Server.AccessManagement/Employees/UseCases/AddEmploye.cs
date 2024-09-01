using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class AddEmploye
{
    [ApiController]
    public sealed class EmployeesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpPost("api/access-management/employees/add")]
        public async Task<ActionResult<Result>> AddEmployeeAsync([FromBody] EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var result = await _mediator.SendAsync(new AddEmployeCommand(employee));

            return Ok(result);
        }
    }

    public sealed record AddEmployeCommand(Employee Employee) : ICommand<Result>;

    private sealed class AddEmployeeCommandHandler(
        IEmployeeReadRepository _employeeReadRepository,
        IEmployeeWriteRepository _employeeWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<AddEmployeCommand, Result>
    {
        public async Task<Result> Handle(AddEmployeCommand request, CancellationToken cancellationToken)
        {
            if (await _employeeReadRepository.ExistsAsync(request.Employee.Id, cancellationToken))
                return Result.AlreadyExists();

            // TODO -> Validation emails and phone numbers

            await _employeeWriteRepository.AddAsync(request.Employee, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
