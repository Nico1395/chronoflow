using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class GetRoleById
{
    internal static IEndpointRouteBuilder UseGetRoleByIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("api/access-management/roles/get-by-id", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromQuery] Guid roleId) =>
        {
            var result = await mediator.SendAsync(new GetRoleByIdQuery(roleId));
            var mappedResult = mapper.MapResult<Role?, RoleDto?>(result);

            return Results.Ok(mappedResult);
        });

        return endpoints;
    }

    public sealed record GetRoleByIdQuery(Guid RoleId) : IQuery<Result<Role?>>;

    internal sealed class GetRoleByIdQueryHandler(IRoleReadRepository _roleReadRepository) : IQueryHandler<GetRoleByIdQuery, Result<Role?>>
    {
        public async Task<Result<Role?>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleReadRepository.GetByIdAsync(request.RoleId, cancellationToken);
            if (role == null)
                return Result.NotFound<Role?>();

            return Result.Okay<Role?>(role);
        }
    }
}
