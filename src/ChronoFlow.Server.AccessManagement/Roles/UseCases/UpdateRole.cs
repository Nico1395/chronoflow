using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class UpdateRole
{
    internal static IEndpointRouteBuilder UseUpdateRoleEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatch("api/access-management/roles/update", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromBody] RoleDto updatedRoleDto) =>
        {
            var updatedRole = mapper.Map<Role>(updatedRoleDto);
            var result = await mediator.SendAsync(new UpdateRoleCommand(updatedRole));

            return Results.Ok(result);
        });

        return endpoints;
    }

    public sealed record UpdateRoleCommand(Role UpdatedRole) : ICommand<Result>;

    internal sealed class UpdateRoleCommandHandler(
        IRoleReadRepository _roleReadRepository,
        IRoleWriteRepository _roleWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<UpdateRoleCommand, Result>
    {
        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _roleReadRepository.GetByIdAsync(request.UpdatedRole.Id, cancellationToken);
            if (existingRole == null)
                return Result.NotFound();

            if (await _roleReadRepository.ExistsWithNameAsync(request.UpdatedRole.Name, cancellationToken))
                return Result.AlreadyExists([ValidationError.AlreadyExists($"TODO -> Localize: A role with the name {request.UpdatedRole.Name} already exists.")]);

            await _roleWriteRepository.UpdateAsync(
                existingRole,
                request.UpdatedRole,
                cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
