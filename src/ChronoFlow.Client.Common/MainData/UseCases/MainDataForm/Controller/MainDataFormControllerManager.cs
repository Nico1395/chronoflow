using ChronoFlow.Client.Common.MainData.Entities;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;

internal sealed class MainDataFormControllerManager : IMainDataFormControllerManager
{
    private readonly Dictionary<Type, object> _mainDataForms = [];

    public void Register<TViewModel>(MainDataForm<TViewModel> mainDataForm)
        where TViewModel : class, IMainDataViewModel
    {
        _mainDataForms[mainDataForm.GetType()] = mainDataForm;
    }

    public void Unregister<TViewModel>(MainDataForm<TViewModel> mainDataForm)
        where TViewModel : class, IMainDataViewModel
    {
        _mainDataForms.Remove(mainDataForm.GetType());
    }

    public MainDataForm<TViewModel> Find<TViewModel>()
        where TViewModel : class, IMainDataViewModel
    {
        return FindInternal<TViewModel>() ?? throw new InvalidProgramException($"A {nameof(IMainDataFormController<TViewModel>)} for view model of type {typeof(TViewModel).FullName} has registered itself to the {nameof(IMainDataFormControllerManager)} yet.");
    }

    private MainDataForm<TViewModel>? FindInternal<TViewModel>()
        where TViewModel : class, IMainDataViewModel
    {
        var genericFormType = typeof(MainDataForm<>).MakeGenericType(typeof(TViewModel));
        return _mainDataForms.TryGetValue(genericFormType, out var form) && form is MainDataForm<TViewModel> mainDataForm ? mainDataForm : null;
    }
}
