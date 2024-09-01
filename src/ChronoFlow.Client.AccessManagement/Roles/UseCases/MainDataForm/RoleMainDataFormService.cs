using ChronoFlow.Client.AccessManagement.Permissions.Entities;
using ChronoFlow.Client.AccessManagement.Permissions.Persistence;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Browser;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;
using ChronoFlow.Client.Common.Notifications;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Roles.UseCases.MainDataForm;

internal sealed class RoleMainDataFormService(
    IPermissionMainDataViewModelService _permissionMainDataViewModelService,
    ILocalNotificationPublisher _localNotificationPublisher,
    IBrowserLogger _browserLogger,
    NavigationManager _navigationManager,
    ILocalizer _localizer,
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
            _permissions = EvaluateGetAllPermissionsResult(result);
        }
        catch (Exception ex)
        {
            _localNotificationPublisher.PublishError("AnUnhandledErrorOccurred");
            await _browserLogger.LogAsync(ex);
            NavigateToList();
        }
    }

    private List<PermissionViewModel>? EvaluateGetAllPermissionsResult(Result<List<PermissionViewModel>> result)
    {
        if (result.Code == ResultCode.Okay && result.Data != null)
        {
            return result.Data;
        }
        else if (result.Code == ResultCode.NotAuthorized)
        {
            _localNotificationPublisher.PublishInfo(_localizer["NotAuthorized"]);
            NavigateToList();
        }
        else
        {
            _localNotificationPublisher.PublishError(_localizer["AnUnhandledErrorOccurred"]);
            NavigateToList();
        }

        return null;
    }

    private void NavigateToList()
    {
        _navigationManager.NavigateTo("main-data/roles");
    }
}
