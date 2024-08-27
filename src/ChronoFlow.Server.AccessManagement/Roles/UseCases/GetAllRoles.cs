using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class GetAllRoles
{
    [ApiController]
    public sealed class RolesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpGet("api/access-management/roles/get-all")]
        public async Task<ActionResult<Result<List<RoleDto>>>> GetAllRolesAsync()
        {
            var result = await _mediator.SendAsync(new GetAllRolesQuery());
            var mappedResult = _mapper.MapResult<List<Role>, List<RoleDto>>(result);

            return Ok(mappedResult);
        }
    }

    public sealed record GetAllRolesQuery() : IQuery<Result<List<Role>>>;

    private sealed class GetAllRolesQueryHandler(IRoleReadRepository _roleReadRepository) : IQueryHandler<GetAllRolesQuery, Result<List<Role>>>
    {
        public async Task<Result<List<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleReadRepository.GetAllAsync(cancellationToken);
            return Result.Okay(roles);
        }
    }
}
