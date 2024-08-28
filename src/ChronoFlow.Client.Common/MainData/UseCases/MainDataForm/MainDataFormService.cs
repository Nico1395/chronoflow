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
            ResponseCode.Okay => MainDataGetNewResultCode.Success,
            ResponseCode.Error => MainDataGetNewResultCode.Error,
            ResponseCode.NotAuthorized => MainDataGetNewResultCode.NotAuthorized,
            _ => MainDataGetNewResultCode.Error,
        };

        return new MainDataGetNewResult<TViewModel>(resultCode, response.Data);
    }

    public virtual async Task<MainDataGetByIdResult<TViewModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _mainDataViewModelService.GetByIdAsync(id, cancellationToken);
        var resultCode = response.Code switch
        {
            ResponseCode.Okay => MainDataGetByIdResultCode.Success,
            ResponseCode.Error => MainDataGetByIdResultCode.Error,
            ResponseCode.NotFound => MainDataGetByIdResultCode.NotFound,
            ResponseCode.NotAuthorized => MainDataGetByIdResultCode.NotAuthorized,
            _ => MainDataGetByIdResultCode.Error,
        };

        return new MainDataGetByIdResult<TViewModel>(resultCode, response.Data);
    }

    public virtual async Task<MainDataAddResult> AddAsync(TViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var response = await _mainDataViewModelService.AddAsync(viewModel, cancellationToken);
        var resultCode = response.Code switch
        {
            ResponseCode.Okay => MainDataAddResultCode.Success,
            ResponseCode.Error => MainDataAddResultCode.Error,
            ResponseCode.AlreadyExists => MainDataAddResultCode.AlreadyExists,
            ResponseCode.ValidationErrors => MainDataAddResultCode.ValidationErrors,
            ResponseCode.NotAuthorized => MainDataAddResultCode.NotAuthorized,
            _ => MainDataAddResultCode.Error,
        };

        return new MainDataAddResult(resultCode);
    }

    public virtual async Task<MainDataUpdateResult> UpdateAsync(TViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var response = await _mainDataViewModelService.AddAsync(viewModel, cancellationToken);
        var resultCode = response.Code switch
        {
            ResponseCode.Okay => MainDataUpdateResultCode.Success,
            ResponseCode.Error => MainDataUpdateResultCode.Error,
            ResponseCode.NotFound => MainDataUpdateResultCode.NotFound,
            ResponseCode.NotAuthorized => MainDataUpdateResultCode.NotAuthorized,
            ResponseCode.AlreadyExists => MainDataUpdateResultCode.AlreadyExists,
            ResponseCode.ValidationErrors => MainDataUpdateResultCode.ValidationErrors,
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
