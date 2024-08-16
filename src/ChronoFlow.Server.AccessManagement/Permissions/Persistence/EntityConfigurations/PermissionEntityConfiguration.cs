using ChronoFlow.Server.AccessManagement.Permissions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoFlow.Server.AccessManagement.Permissions.Persistence.EntityConfigurations;

internal sealed class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("access_management_permissions");
        builder.HasKey(x => x.Id).HasName("pk_access_management_permissions");

        builder.Property(x => x.Id).HasColumnName("id").UseIdentityAlwaysColumn().IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
    }
}
