using ChronoFlow.Server.AccessManagement.Employees.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.AccessManagement.Employees.UseCases;

public static class DeleteEmployee
{
    internal static IEndpointRouteBuilder UseDeleteEmployeeEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("api/access-management/employees/delete", async ([FromServices] IMediator mediator, [FromQuery] Guid employeeId) =>
        {
            var result = await mediator.SendAsync(new DeleteEmployeeCommand(employeeId));
            return Results.Ok(result);
        });

        return endpoints;
    }

    public sealed record DeleteEmployeeCommand(Guid EmployeeId) : ICommand<Result>;

    internal sealed class DeleteEmployeeCommandHandler(
        IEmployeeReadRepository _employeeReadRepository,
        IEmployeeWriteRepository _employeeWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<DeleteEmployeeCommand, Result>
    {
        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeReadRepository.GetByIdAsync(request.EmployeeId, cancellationToken);
            if (employee == null)
                return Result.NotFound();

            await _employeeWriteRepository.DeleteAsync(employee, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
