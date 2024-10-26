using ChronoFlow.Server.EmployeeManagement.Employees;
using ChronoFlow.Shared.Common.Mapping.Configuration;
using ChronoFlow.Shared.EmployeeManagement.Employees;

namespace ChronoFlow.Server.EmployeeManagement;

internal sealed class EmployeeManagementMappingProfile : IMappingProfile
{
    public void ConfigureMapping(IMappingProfileConfiguration configuration)
    {
        configuration.ConfigureTypes<Employee, EmployeeDto>();
        configuration.ConfigureTypes<EmployeeEmailAddress, EmployeeEmailAddressDto>();
        configuration.ConfigureTypes<EmployeePhoneNumber, EmployeePhoneNumberDto>();
    }
}
