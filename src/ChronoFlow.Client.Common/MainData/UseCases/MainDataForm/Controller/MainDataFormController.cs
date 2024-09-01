using ChronoFlow.Client.Common.MainData.Entities;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;

internal sealed class MainDataFormController<TViewModel>(IMainDataFormControllerManager _mainDataFormControllerManager) : IMainDataFormController<TViewModel>
    where TViewModel : class, IMainDataViewModel
{
    private MainDataForm<TViewModel> MainDataForm => _mainDataFormControllerManager.Find<TViewModel>();

    public Guid? ItemId => MainDataForm.ItemId;
    public bool IsNew => MainDataForm.IsNew;
    public bool IsBusy => MainDataForm.IsBusy;
    public TViewModel? Item => MainDataForm.Item;
    public MainDataFormContext<TViewModel>? Context => MainDataForm.Context;

    public void Render()
    {
        MainDataForm.Render();
    }

    public Task SetBusyAsync(bool busy)
    {
        return MainDataForm.SetBusyAsync(busy);
    }
}
