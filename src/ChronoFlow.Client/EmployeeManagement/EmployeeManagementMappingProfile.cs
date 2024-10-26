using ChronoFlow.Client.EmployeeManagement.Employees;
using ChronoFlow.Shared.Common.Mapping.Configuration;
using ChronoFlow.Shared.EmployeeManagement.Employees;

namespace ChronoFlow.Client.EmployeeManagement;

internal sealed class EmployeeManagementMappingProfile : IMappingProfile
{
    public void ConfigureMapping(IMappingProfileConfiguration configuration)
    {
        configuration.ConfigureTypes<EmployeeModel, EmployeeDto>();
        configuration.ConfigureTypes<EmployeeEmailAddressModel, EmployeeEmailAddressDto>();
        configuration.ConfigureTypes<EmployeePhoneNumberModel, EmployeePhoneNumberDto>();
    }
}
