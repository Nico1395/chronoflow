using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class GetRoleById
{
    [ApiController]
    public sealed class RolesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpGet("api/access-management/roles/get-by-id")]
        public async Task<ActionResult<Result<RoleDto>>> GetRoleByIdAsync([FromQuery] Guid roleId)
        {
            var result = await _mediator.SendAsync(new GetRoleByIdQuery(roleId));
            var mappedResult = _mapper.Map<Role, RoleDto>(result);

            return Ok(mappedResult);
        }
    }

    public sealed record GetRoleByIdQuery(Guid RoleId) : IQuery<Result<Role>>;

    private sealed class GetRoleByIdQueryHandler(IRoleReadRepository _roleReadRepository) : IQueryHandler<GetRoleByIdQuery, Result<Role>>
    {
        public async Task<Result<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleReadRepository.GetByIdAsync(request.RoleId, cancellationToken);
            if (role == null)
                return Result.NotFound<Role>();

            return Result.Okay(role);
        }
    }
}
