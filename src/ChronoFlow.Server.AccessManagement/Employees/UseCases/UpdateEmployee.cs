using ChronoFlow.Server.AccessManagement.Employees.Entities;
using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.AccessManagement.Employees;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class UpdateEmployee
{
    internal static IEndpointRouteBuilder UseUpdateEmployeeEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatch("api/access-management/employees/update", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromBody] EmployeeDto updatedEmployeeDto) =>
        {
            var updatedEmployee = mapper.Map<Employee>(updatedEmployeeDto);
            var result = await mediator.SendAsync(new UpdateEmployeeCommand(updatedEmployee));

            return Results.Ok(result);
        });

        return endpoints;
    }

    public sealed record UpdateEmployeeCommand(Employee UpdatedEmployee) : ICommand<Result>;

    internal sealed class UpdateEmployeeCommandHandler(
        IEmployeeReadRepository _employeeReadRepository,
        IEmployeeWriteRepository _employeeWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<UpdateEmployeeCommand, Result>
    {
        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeReadRepository.GetByIdAsync(request.UpdatedEmployee.Id, cancellationToken);
            if (existingEmployee == null)
                return Result.NotFound();

            await _employeeWriteRepository.UpdateAsync(
                existingEmployee,
                request.UpdatedEmployee,
                cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
