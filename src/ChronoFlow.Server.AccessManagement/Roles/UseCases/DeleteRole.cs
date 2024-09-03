using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class DeleteRole
{
    [ApiController]
    public sealed class RolesController(IMediator _mediator) : ControllerBase
    {
        [HttpDelete("api/access-management/roles/delete")]
        public async Task<ActionResult<Result>> DeleteRoleAsync([FromQuery] Guid roleId)
        {
            var result = await _mediator.SendAsync(new DeleteRoleCommand(roleId));
            return Ok(result);
        }
    }

    public sealed record DeleteRoleCommand(Guid RoleId) : ICommand<Result>;

    private sealed class DeleteRoleCommandHandler(
        IRoleReadRepository _roleReadRepository,
        IRoleWriteRepository _roleWriteRepository,
        IUnitOfWork _unitOfWork) : ICommandHandler<DeleteRoleCommand, Result>
    {
        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleReadRepository.GetByIdEagerAsync(request.RoleId, cancellationToken);
            if (role == null)
                return Result.NotFound();

            await _roleWriteRepository.DeleteAsync(role, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Okay();
        }
    }
}
