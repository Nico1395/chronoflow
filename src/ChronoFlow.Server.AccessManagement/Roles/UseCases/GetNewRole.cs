using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class GetNewRole
{
    [ApiController]
    public sealed class RolesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpGet("api/access-management/roles/get-new")]
        public async Task<ActionResult<Result<RoleDto>>> GetNewRoleAsync()
        {
            var result = await _mediator.SendAsync(new GetNewRoleQuery());
            var mappedResult = _mapper.MapResult<Role, RoleDto>(result);

            return Ok(mappedResult);
        }
    }

    public sealed record GetNewRoleQuery() : IQuery<Result<Role>>;

    private sealed record GetNewRoleQueryHandler() : IQueryHandler<GetNewRoleQuery, Result<Role>>
    {
        public Task<Result<Role>> Handle(GetNewRoleQuery request, CancellationToken cancellationToken)
        {
            var role = new Role();
            return Task.FromResult(Result.Okay(role));
        }
    }
}
