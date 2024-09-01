using ChronoFlow.Server.AccessManagement.Permissions.Entities;
using ChronoFlow.Server.AccessManagement.Permissions.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.AccessManagement.Permissions;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Permissions.UseCases;

public static class GetAllPermissions
{
    [ApiController]
    public sealed class PermissionsController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpGet("api/access-management/permissions/get-all")]
        public async Task<ActionResult<Result<List<PermissionDto>>>> GetAllPermissionsAsync()
        {
            var result = await _mediator.SendAsync(new GetAllPermissionsQuery());
            var mappedResult = _mapper.MapResult<List<Permission>, List<PermissionDto>>(result);

            return Ok(mappedResult);
        }
    }

    public sealed record GetAllPermissionsQuery() : IQuery<Result<List<Permission>>>;

    internal sealed class GetAllPermissionsQueryHandler(IPermissionReadRepository _permissionReadRepository) : IQueryHandler<GetAllPermissionsQuery, Result<List<Permission>>>
    {
        public async Task<Result<List<Permission>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionReadRepository.GetAllAsync(cancellationToken);
            return Result.Okay(permissions);
        }
    }
}
