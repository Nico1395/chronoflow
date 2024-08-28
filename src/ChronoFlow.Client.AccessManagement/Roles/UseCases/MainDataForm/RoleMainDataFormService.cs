using ChronoFlow.Client.AccessManagement.Permissions.Entities;
using ChronoFlow.Client.AccessManagement.Permissions.Persistence;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataForm;

internal sealed class RoleMainDataFormService(
    IPermissionMainDataViewModelService _permissionMainDataViewModelService,
    IMainDataViewModelService<RoleViewModel> mainDataViewModelService,
    IMainDataFormController<RoleViewModel> mainDataFormController) : MainDataFormService<RoleViewModel>(mainDataViewModelService, mainDataFormController), IRoleMainDataFormService
{
    private List<PermissionViewModel>? _permissions;

    public List<PermissionViewModel> GetPermissions()
    {
        return _permissions ?? [];
    }

    public override async Task OnLoadedAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _permissionMainDataViewModelService.GetAllAsync(cancellationToken);
            // TODO -> Evaluate

            _permissions = result.Data;
            _formController.Render();
        }
        catch (Exception ex)
        {

        }
    }
}
