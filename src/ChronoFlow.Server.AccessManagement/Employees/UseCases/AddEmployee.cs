using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class AddEmployee
{
    [ApiController]
    public sealed class EmployeesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpPost("api/access-management/employees/add")]
        public async Task<ActionResult<Result>> AddEmployeeAsync([FromBody] EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var result = await _mediator.SendAsync(new AddEmployeeCommand(employee));

            return Ok(result);
        }
    }

    public sealed record AddEmployeeCommand(Employee Employee) : ICommand<Result>;

    private sealed class AddEmployeeCommandHandler(
        IEmployeeReadRepository _employeeReadRepository,
        IEmployeeWriteRepository _employeeWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<AddEmployeeCommand, Result>
    {
        public async Task<Result> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _employeeReadRepository.ExistsAsync(request.Employee.Id, cancellationToken))
                    return Result.AlreadyExists();

                // TODO -> Validation emails and phone numbers

                await _employeeWriteRepository.AddAsync(request.Employee, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return Result.Okay();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
