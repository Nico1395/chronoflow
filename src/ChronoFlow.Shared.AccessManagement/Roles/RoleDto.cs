using ChronoFlow.Shared.AccessManagement.Permissions;

namespace ChronoFlow.Shared.AccessManagement.Roles;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<PermissionDto> Permissions { get; set; } = [];
    public DateTime Created { get; set; }
    public DateTime LastChanged { get; set; }
}
