using ChronoFlow.Client.Common.MainData.Persistence;
using ChronoFlow.Client.Common.MainData.Results;
using ChronoFlow.Shared.Common.Messaging;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public class MainDataListService<TViewModel>(IMainDataViewModelService<TViewModel> _viewModelService) : IMainDataListService<TViewModel>
    where TViewModel : class
{
    public virtual async Task<MainDataGetAllResult<TViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await _viewModelService.GetAllAsync(cancellationToken);
        var resultCode = response.Code switch
        {
            ResultCode.Okay => MainDataGetAllResultCode.Success,
            ResultCode.Error => MainDataGetAllResultCode.Error,
            ResultCode.NotAuthorized => MainDataGetAllResultCode.NotAuthorized,
            _ => MainDataGetAllResultCode.Error
        };

        return new MainDataGetAllResult<TViewModel>(resultCode, response.Data);
    }

    public virtual async Task<MainDataDeleteResult> DeleteAsync(TViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var response = await _viewModelService.DeleteAsync(viewModel, cancellationToken);
        var resultCode = response.Code switch
        {
            ResultCode.Okay => MainDataDeleteResultCode.Success,
            ResultCode.Error => MainDataDeleteResultCode.Error,
            ResultCode.NotFound => MainDataDeleteResultCode.NotFound,
            ResultCode.NotAuthorized => MainDataDeleteResultCode.NotAuthorized,
            _ => MainDataDeleteResultCode.Error
        };

        return new MainDataDeleteResult(resultCode, response.Message);
    }
}
