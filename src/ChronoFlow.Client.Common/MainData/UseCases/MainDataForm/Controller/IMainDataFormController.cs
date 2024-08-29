using ChronoFlow.Client.Common.MainData.Entities;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;

public interface IMainDataFormController<TViewModel>
    where TViewModel : class, IMainDataViewModel
{
    public Guid? ItemId { get; }
    public bool IsNew { get; }
    public bool IsBusy { get; }
    public TViewModel? Item { get; }
    public MainDataFormContext<TViewModel>? Context { get; }
    public void Render();
    public Task SetBusyAsync(bool busy);
}
