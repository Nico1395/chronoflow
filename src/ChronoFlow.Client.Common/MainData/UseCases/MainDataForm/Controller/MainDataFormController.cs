using ChronoFlow.Client.Common.MainData.Entities;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;

internal sealed class MainDataFormController<TViewModel>(IMainDataFormControllerManager _mainDataFormControllerManager) : IMainDataFormController<TViewModel>
    where TViewModel : class, IMainDataViewModel
{
    public Guid? ItemId => _mainDataFormControllerManager.Find<TViewModel>().ItemId;
    public bool IsNew => _mainDataFormControllerManager.Find<TViewModel>().IsNew;
    public bool IsBusy => _mainDataFormControllerManager.Find<TViewModel>().IsBusy;
    public TViewModel? Item => _mainDataFormControllerManager.Find<TViewModel>().Item;
    public MainDataFormContext<TViewModel> Context { get; }

    public void Render()
    {
        _mainDataFormControllerManager.Find<TViewModel>().Render();
    }

    public Task SetBusyAsync(bool busy)
    {
        return _mainDataFormControllerManager.Find<TViewModel>().SetBusyAsync(busy);
    }
}
