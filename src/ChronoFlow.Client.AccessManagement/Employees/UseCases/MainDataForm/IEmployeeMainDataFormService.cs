using ChronoFlow.Client.AccessManagement.Roles.Entities;

namespace ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataForm;

internal interface IEmployeeMainDataFormService
{
    internal List<RoleViewModel> GetRoles();
}
