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
        builder.HasIndex(e => new { e.PersonnelNumber }).IsUnique();

        builder.Property(s => s.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.PersonnelNumber).HasColumnName("personnel_number").HasMaxLength(50).IsRequired();
        builder.Property(f => f.PasswordHash).HasColumnName("password_hash").HasMaxLength(250).IsRequired();
        builder.ComplexProperty(d => d.Name, b =>
        {
            b.Property(c => c.FirstName).HasColumnName("name_first_name").HasMaxLength(100).IsRequired();
            b.Property(c => c.LastName).HasColumnName("name_last_name").HasMaxLength(100).IsRequired();
        });
        builder.ComplexProperty(d => d.Address, b =>
        {
            b.Property(a => a.Street).HasColumnName("address_street").HasMaxLength(100);
            b.Property(a => a.HouseNumber).HasColumnName("address_house_number").HasMaxLength(25);
            b.Property(a => a.City).HasColumnName("address_city").HasMaxLength(100);
            b.Property(a => a.PostalCode).HasColumnName("address_postal_code").HasMaxLength(50);
            b.Property(a => a.State).HasColumnName("address_state").HasMaxLength(100);
            b.Property(a => a.Country).HasColumnName("address_country").HasMaxLength(100);
        });
        builder.Property(x => x.Birthday).HasColumnName("birthday");
        builder.Property(x => x.Created).HasColumnName("created").IsRequired();
        builder.Property(x => x.LastChanged).HasColumnName("last_changed").IsRequired();
        builder.HasMany(d => d.Emails).WithOne().HasForeignKey(e => e.EmployeeId).HasConstraintName("fk_access_management_employee_emails").OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(d => d.PhoneNumbers).WithOne().HasForeignKey(e => e.EmployeeId).HasConstraintName("fk_access_management_employee_phone_numbers").OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(d => d.EmployeeRoles).WithOne(e => e.Employee).HasForeignKey(r => r.EmployeeId).HasConstraintName("fk_access_management_employee_employee_roles").OnDelete(DeleteBehavior.Cascade);
    }
}
