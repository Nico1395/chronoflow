using ChronoFlow.Client.AccessManagement.Permissions.Entities;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataForm;

internal interface IRoleMainDataFormService
{
    public List<PermissionViewModel> GetPermissions();
}
