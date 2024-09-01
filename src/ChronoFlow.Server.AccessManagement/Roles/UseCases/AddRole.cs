using ChronoFlow.Server.AccessManagement.Roles.Entities;
using ChronoFlow.Server.AccessManagement.Roles.Entities.Repositories;
using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Server.Common.Persistence;
using ChronoFlow.Shared.AccessManagement.Roles;
using ChronoFlow.Shared.Common.Messaging;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.AccessManagement.Roles.UseCases;

public static class AddRole
{
    [ApiController]
    public sealed class RolesController(IMediator _mediator, IMapper _mapper) : ControllerBase
    {
        [HttpPost("api/access-management/roles/add")]
        public async Task<ActionResult<Result>> AddRoleAsync([FromBody] RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            var result = await _mediator.SendAsync(new AddRoleCommand(role));

            return Ok(result);
        }
    }

    public sealed record AddRoleCommand(Role Role) : ICommand<Result>;

    private sealed class AddRoleCommandHandler(
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

