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

public static class AddEmploye
{
    internal static IEndpointRouteBuilder UseAddEmployeeEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/access-management/employees/add", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromBody] EmployeeDto employeeDto) =>
        {
            var employee = mapper.Map<Employee>(employeeDto);
            var result = await mediator.SendAsync(new AddEmployeCommand(employee));

            return Results.Ok(result);
        });

        return endpoints;
    }

    public sealed record AddEmployeCommand(Employee Employee) : ICommand<Result>;

    internal sealed class AddEmployeeCommandHandler(
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
