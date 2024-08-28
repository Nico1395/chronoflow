using ChronoFlow.Client.Common.MainData.Entities;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;

internal interface IMainDataFormControllerManager
{
    internal void Register<TViewModel>(MainDataForm<TViewModel> mainDataForm)
        where TViewModel : class, IMainDataViewModel;

    internal void Unregister<TViewModel>(MainDataForm<TViewModel> mainDataForm)
        where TViewModel : class, IMainDataViewModel;

    internal MainDataForm<TViewModel> Find<TViewModel>()
        where TViewModel : class, IMainDataViewModel;
}
