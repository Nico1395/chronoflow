using ChronoFlow.Shared.Common.Dtos;

namespace ChronoFlow.Shared.AccessManagement.Roles;

public sealed class RoleModel : MainDataAggregateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<RolePermissionDto> RolePermissions { get; init; } = [];
}
