using ChronoFlow.Client.AccessManagement.Permissions.Entities;

namespace ChronoFlow.Client.AccessManagement.Roles.Entities;

public class RoleViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<PermissionViewModel> Permissions { get; set; } = [];
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastChanged { get; set; } = DateTime.Now;
}
