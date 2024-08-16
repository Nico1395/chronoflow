using ChronoFlow.Server.AccessManagement.Permissions.Entities;

namespace ChronoFlow.Server.AccessManagement.Roles.Entities;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Permission> Permissions { get; set; } = [];
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastChanged { get; set; } = DateTime.Now;
}
