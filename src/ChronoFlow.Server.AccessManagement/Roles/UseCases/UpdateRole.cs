using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Mapping;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class UpdateRole
{
    [ApiController]
    public sealed class RolesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpPatch("api/access-management/roles/update")]
        public async Task<ActionResult<Result>> UpdateRoleAsync([FromBody] RoleDto updatedRoleDto)
        {
            var updatedRole = _mapper.Map<Role>(updatedRoleDto);
            var result = await _mediator.SendAsync(new UpdateRoleCommand(updatedRole));

            return Ok(result);
        }
    }

    public sealed record UpdateRoleCommand(Role UpdatedRole) : ICommand<Result>;

    private sealed class UpdateRoleCommandHandler(
        IRoleReadRepository _roleReadRepository,
        IRoleWriteRepository _roleWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<UpdateRoleCommand, Result>
    {
        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _roleReadRepository.GetByIdAsync(request.UpdatedRole.Id, cancellationToken);
            if (existingRole == null)
                return Result.NotFound();

            var nameHasChanged = existingRole.Name != request.UpdatedRole.Name;
            if (nameHasChanged && await _roleReadRepository.ExistsWithNameAsync(request.UpdatedRole.Name, cancellationToken))
                return Result.AlreadyExists([ValidationError.AlreadyExists($"TODO -> Localize: A role with the name {request.UpdatedRole.Name} already exists.")]);

            request.UpdatedRole.LastChanged = DateTime.Now;

            await _roleWriteRepository.UpdateAsync(
                existingRole,
                request.UpdatedRole,
                cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
