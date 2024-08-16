using ChronoFlow.Server.AccessManagement.Employees.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence.EntityConfigurations;

internal sealed class EmployeePhoneNumberEntityConfiguration : IEntityTypeConfiguration<EmployeePhoneNumber>
{
    public void Configure(EntityTypeBuilder<EmployeePhoneNumber> builder)
    {
        builder.ToTable("access_management_employee_phone_numbers");
        builder.HasKey(d => new { d.EmployeeId, d.PhoneNumber }).HasName("pk_access_management_employee_phone_numbers");

        builder.Property(d => d.EmployeeId).HasColumnName("employee_id").IsRequired();
        builder.Property(d => d.PhoneNumber).HasColumnName("phone_number").HasMaxLength(50).IsRequired();
    }
}
