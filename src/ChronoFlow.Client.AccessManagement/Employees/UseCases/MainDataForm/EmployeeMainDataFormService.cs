using ChronoFlow.Client.AccessManagement.Employees.Entities;
using ChronoFlow.Client.AccessManagement.Roles.Entities;
using ChronoFlow.Client.Common.Browser;
using ChronoFlow.Client.Common.Localization;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;
using ChronoFlow.Client.Common.Notifications;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Components;

namespace ChronoFlow.Client.AccessManagement.Employees.UseCases.MainDataForm;

internal sealed class EmployeeMainDataFormService(
    IMainDataViewModelService<RoleViewModel> roleMainDataViewModelService,
    ILocalNotificationPublisher _localNotificationPublisher,
    IBrowserLogger _browserLogger,
    NavigationManager _navigationManager,
    ILocalizer _localizer,
    IMainDataViewModelService<EmployeeViewModel> mainDataViewModelService,
    IMainDataFormController<EmployeeViewModel> mainDataFormController) : MainDataFormService<EmployeeViewModel>(mainDataViewModelService, mainDataFormController), IEmployeeMainDataFormService
{
    private List<RoleViewModel>? _roles;

    public List<RoleViewModel> GetRoles()
    {
        return _roles ?? [];
    }

    public override async Task OnLoadedAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await roleMainDataViewModelService.GetAllAsync(cancellationToken);
            _roles = EvaluateGetAllRolesResult(result);
        }
        catch (Exception ex)
        {
            _localNotificationPublisher.PublishError("AnUnhandledErrorOccurred");
            await _browserLogger.LogAsync(ex);
            NavigateToList();
        }
    }

    private List<RoleViewModel>? EvaluateGetAllRolesResult(Result<List<RoleViewModel>> result)
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
        _navigationManager.NavigateTo("main-data/employees");
    }
}
