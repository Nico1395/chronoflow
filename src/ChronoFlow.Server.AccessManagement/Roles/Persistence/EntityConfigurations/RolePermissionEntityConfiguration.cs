using ChronoFlow.Server.AccessManagement.Roles.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoFlow.Server.AccessManagement.Roles.Persistence.EntityConfigurations;

internal sealed class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("access_management_role_permissions");
        builder.HasKey(f => new { f.RoleId, f.PermissionId }).HasName("pk_access_management_role_permissions");

        builder.Property(f => f.RoleId).HasColumnName("role_id").IsRequired();
        builder.Property(f => f.PermissionId).HasColumnName("permission_id").IsRequired();
    }
}
