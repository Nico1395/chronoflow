using ChronoFlow.Client.Common.MainData.Entities;
using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Client.Common.MainData.Results;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;
using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;

public class MainDataFormService<TViewModel>(
    IMainDataViewModelService<TViewModel> mainDataViewModelService,
    IMainDataFormController<TViewModel> mainDataFormController) : IMainDataFormService<TViewModel>
    where TViewModel : class, IMainDataViewModel
{
    protected readonly IMainDataViewModelService<TViewModel> _mainDataViewModelService = mainDataViewModelService;
    protected readonly IMainDataFormController<TViewModel> _formController = mainDataFormController;

    public virtual async Task<MainDataGetNewResult<TViewModel>> GetNewAsync(CancellationToken cancellationToken = default)
    {
        var response = await _mainDataViewModelService.GetNewAsync(cancellationToken);
        var resultCode = response.Code switch
        {
            ResultCode.Okay => MainDataGetNewResultCode.Success,
            ResultCode.Error => MainDataGetNewResultCode.Error,
            ResultCode.NotAuthorized => MainDataGetNewResultCode.NotAuthorized,
            _ => MainDataGetNewResultCode.Error,
        };

        return new MainDataGetNewResult<TViewModel>(resultCode, response.Data);
    }

    public virtual async Task<MainDataGetByIdResult<TViewModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _mainDataViewModelService.GetByIdAsync(id, cancellationToken);
        var resultCode = response.Code switch
        {
            ResultCode.Okay => MainDataGetByIdResultCode.Success,
            ResultCode.Error => MainDataGetByIdResultCode.Error,
            ResultCode.NotFound => MainDataGetByIdResultCode.NotFound,
            ResultCode.NotAuthorized => MainDataGetByIdResultCode.NotAuthorized,
            _ => MainDataGetByIdResultCode.Error,
        };

        return new MainDataGetByIdResult<TViewModel>(resultCode, response.Data);
    }

    public virtual async Task<MainDataAddResult> AddAsync(TViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var response = await _mainDataViewModelService.AddAsync(viewModel, cancellationToken);
        var resultCode = response.Code switch
        {
            ResultCode.Okay => MainDataAddResultCode.Success,
            ResultCode.Error => MainDataAddResultCode.Error,
            ResultCode.AlreadyExists => MainDataAddResultCode.AlreadyExists,
            ResultCode.ValidationErrors => MainDataAddResultCode.ValidationErrors,
            ResultCode.NotAuthorized => MainDataAddResultCode.NotAuthorized,
            _ => MainDataAddResultCode.Error,
        };

        return new MainDataAddResult(resultCode);
    }

    public virtual async Task<MainDataUpdateResult> UpdateAsync(TViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var response = await _mainDataViewModelService.UpdateAsync(viewModel, cancellationToken);
        var resultCode = response.Code switch
        {
            ResultCode.Okay => MainDataUpdateResultCode.Success,
            ResultCode.Error => MainDataUpdateResultCode.Error,
            ResultCode.NotFound => MainDataUpdateResultCode.NotFound,
            ResultCode.NotAuthorized => MainDataUpdateResultCode.NotAuthorized,
            ResultCode.AlreadyExists => MainDataUpdateResultCode.AlreadyExists,
            ResultCode.ValidationErrors => MainDataUpdateResultCode.ValidationErrors,
            _ => MainDataUpdateResultCode.Error,
        };

        return new MainDataUpdateResult(resultCode);
    }

    public virtual Task OnLoadedAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnSavedAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
