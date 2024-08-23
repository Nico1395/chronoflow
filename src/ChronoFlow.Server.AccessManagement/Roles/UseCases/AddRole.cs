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

public static class AddRole
{
    internal static IEndpointRouteBuilder UseAddRoleEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/access-management/roles/add", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromBody] RoleDto roleDto) =>
        {
            var role = mapper.Map<Role>(roleDto);
            var result = await mediator.SendAsync(new AddRoleCommand(role));

            return Results.Ok(result);
        });

        return endpoints;
    }

    public sealed record AddRoleCommand(Role Role) : ICommand<Result>;

    internal sealed class AddRoleCommandHandler(
        IRoleReadRepository _roleReadRepository,
        IRoleWriteRepository _roleWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<AddRoleCommand, Result>
    {
        public async Task<Result> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            if (await _roleReadRepository.ExistsAsync(request.Role.Id, cancellationToken))
                return Result.AlreadyExists();

            if (await _roleReadRepository.ExistsWithNameAsync(request.Role.Name, cancellationToken))
                return Result.AlreadyExists([ValidationError.AlreadyExists($"TODO -> Localize: A role with the name {request.Role.Name} already exists.")]);

            await _roleWriteRepository.AddAsync(request.Role, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}

