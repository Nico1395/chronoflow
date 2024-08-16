using ChronoFlow.Server.AccessManagement.Employees.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence.EntityConfigurations;

internal sealed class EmployeeEmailEntityConfiguration : IEntityTypeConfiguration<EmployeeEmail>
{
    public void Configure(EntityTypeBuilder<EmployeeEmail> builder)
    {
        builder.ToTable("access_management_employee_emails");
        builder.HasKey(d => new { d.EmployeeId, d.Email }).HasName("pk_access_management_employee_emails");
        builder.HasIndex(d => d.Email).IsUnique();

        builder.Property(d => d.EmployeeId).HasColumnName("employee_id").IsRequired();
        builder.Property(d => d.Email).HasColumnName("email").HasMaxLength(50).IsRequired();
        builder.Property(d => d.IsPrimary).HasColumnName("is_primary").IsRequired();

    }
}
