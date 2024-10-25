﻿namespace ChronoFlow.Client.EmployeeManagement.Employees;

public sealed class EmployeePhoneNumberModel
{
    public required Guid EmployeeId { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsPrimary { get; set; }
}