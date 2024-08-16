using ChronoFlow.Server.AccessManagement.Employees.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoFlow.Server.AccessManagement.Employees.Persistence.EntityConfigurations;

internal sealed class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("access_management_employees");
        builder.HasKey(e => e.Id).HasName("pk_access_management_employees");

        builder.Property(s => s.Id).HasColumnName("id").IsRequired();
        builder.ComplexProperty(e => e.Credentials, b =>
        {
            b.Property(d => d.PersonnelNumber).HasColumnName("credentials_personnel_number").HasMaxLength(50).IsRequired();
            b.Property(f => f.PasswordHash).HasColumnName("credentials_password_hash").HasMaxLength(250).IsRequired();
        });
        builder.ComplexProperty(d => d.Name, b =>
        {
            b.Property(c => c.FirstName).HasColumnName("name_first_name").HasMaxLength(100).IsRequired();
            b.Property(c => c.LastName).HasColumnName("name_last_name").HasMaxLength(100).IsRequired();
        });
        builder.Property(x => x.Birthday).HasColumnName("birthday");
        builder.Property(x => x.Created).HasColumnName("created").IsRequired();
        builder.Property(x => x.LastChanged).HasColumnName("last_changed").IsRequired();
        builder.HasMany(d => d.Emails).WithOne().HasForeignKey(e => e.EmployeeId).HasForeignKey("fk_access_management_employee_emails");
        builder.HasMany(d => d.PhoneNumbers).WithOne().HasForeignKey(e => e.EmployeeId).HasForeignKey("fk_access_management_employee_phone_numbers");
        builder.HasMany(d => d.Roles).WithMany().UsingEntity<EmployeeRole>();
    }
}
