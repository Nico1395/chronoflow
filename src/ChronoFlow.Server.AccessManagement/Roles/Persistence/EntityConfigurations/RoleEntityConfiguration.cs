using ChronoFlow.Server.AccessManagement.Roles.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoFlow.Server.AccessManagement.Roles.Persistence.EntityConfigurations;

internal sealed class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("access_management_roles");
        builder.HasKey(x => x.Id).HasName("pk_access_management_roles");
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Id).HasColumnName("id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(200).HasColumnName("description");
        builder.Property(x => x.Created).HasColumnName("created").IsRequired();
        builder.Property(x => x.LastChanged).HasColumnName("last_changed").IsRequired();
    }
}
