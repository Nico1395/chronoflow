using ChronoFlow.Server.AccessManagement.Employees.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence.EntityConfigurations;

internal sealed class EmployeeRoleEntityConfiguration : IEntityTypeConfiguration<EmployeeRole>
{
    public void Configure(EntityTypeBuilder<EmployeeRole> builder)
    {
        builder.ToTable("access_management_employee_roles");
        builder.HasKey(d => new { d.EmployeeId, d.RoleId }).HasName("pk_access_management_employee_roles");

        builder.Property(d => d.EmployeeId).HasColumnName("employee_id").IsRequired();
        builder.Property(d => d.RoleId).HasColumnName("role_id").IsRequired();

        builder.HasOne(r => r.Role).WithMany().HasForeignKey(r => r.RoleId).HasConstraintName("fk_access_management_employee_roles_roles").OnDelete(DeleteBehavior.Restrict);
    }
}
