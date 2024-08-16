using ChronoFlow.Server.Common.Objects.ValueObjects;

namespace ChronoFlow.Server.AccessManagement.Employees.Entities;

public record EmployeeAddress
{
    public Guid EmployeeId { get; set; }
    public Address Address { get; set; } = Address.Empty();
}
