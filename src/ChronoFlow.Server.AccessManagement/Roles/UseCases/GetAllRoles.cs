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

public static class GetAllRoles
{
    internal static IEndpointRouteBuilder UseGetAllRolesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("api/access-management/roles/get-all", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper) =>
        {
            var result = await mediator.SendAsync(new GetAllRolesQuery());
            var mappedResult = mapper.MapResult<List<Role>, List<RoleDto>>(result);

            return Results.Ok(mappedResult);
        });

        return endpoints;
    }

    public sealed record GetAllRolesQuery() : IQuery<Result<List<Role>>>;

    internal sealed class GetAllRolesQueryHandler(IRoleReadRepository _roleReadRepository) : IQueryHandler<GetAllRolesQuery, Result<List<Role>>>
    {
        public async Task<Result<List<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleReadRepository.GetAllAsync(cancellationToken);
            return Result.Okay(roles);
        }
    }
}
