using ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataList;
using ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataList;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataPage.Configuration;

namespace ChronoFlow.Client.AccessManagement;

internal sealed class MainDataMenuProfile : MainDataMenuProfileBase
{
    public override void Configure(MainDataMenuConfiguration configuration)
    {
        configuration.AddCategory(c => c.WithTitle("AccessManagement")
            .AddItem<EmployeeMainDataList>(i => i
                .WithUri("main-data/employees")
                .WithTitle("Employees")
                .WithIconLeft("bi bi-person-fill"))
            .AddItem<RoleMainDataList>(i => i
                .WithUri("main-data/roles")
                .WithTitle("Roles")
                .WithIconLeft("bi bi-person-vcard-fill")));

        configuration.AddCategory(c => c.WithTitle("Locations")
            .AddItem<RoleMainDataList>(i => i
                .WithUri("main-data/locations")
                .WithTitle("Locations")
                .WithIconLeft("bi bi-buildings-fill"))
            .AddItem<RoleMainDataList>(i => i
                .WithUri("main-data/workplaces")
                .WithTitle("Workplaces")
                .WithIconLeft("bi bi-pc-display")));
    }
}
