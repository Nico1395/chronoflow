using ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataList;
using ChronoFlow.Client.AccessManagement.Roles.UseCases;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataPage;

namespace ChronoFlow.Client.AccessManagement;

internal sealed class MainDataMenuProfile : MainDataMenuProfileBase
{
    public override void Configure(MainDataMenuConfiguration configuration)
    {
        configuration.AddDomainObjectList<EmployeeMainDataList>("employees", "Employees", "AccessManagement", "main-data/employees");

        configuration.AddDomainObjectList<RoleMainDataList>("roles", "Roles", "AccessManagement", "main-data/roles");


        configuration.AddDomainObjectList<RoleMainDataList>("locations", "Locations", "Locations", "main-data/locations");
        configuration.AddDomainObjectList<RoleMainDataList>("workplaces", "Workplaces", "Locations", "main-data/workplaces");
    }
}
