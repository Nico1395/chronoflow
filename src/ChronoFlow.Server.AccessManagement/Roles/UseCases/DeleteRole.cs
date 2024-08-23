using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class DeleteRole
{
    internal static IEndpointRouteBuilder UseDeleteRoleEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("api/access-management/roles/delete", async ([FromServices] IMediator mediator, [FromQuery] Guid roleId) =>
        {
            var result = await mediator.SendAsync(new DeleteRoleCommand(roleId));
            return Results.Ok(result);
        });

        return endpoints;
    }

    public sealed record DeleteRoleCommand(Guid RoleId) : ICommand<Result>;

    internal sealed class DeleteRoleCommandHandler(
        IRoleReadRepository _roleReadRepository,
        IRoleWriteRepository _roleWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<DeleteRoleCommand, Result>
    {
        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleReadRepository.GetByIdAsync(request.RoleId, cancellationToken);
            if (role == null)
                return Result.NotFound();

            await _roleWriteRepository.DeleteAsync(role, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
