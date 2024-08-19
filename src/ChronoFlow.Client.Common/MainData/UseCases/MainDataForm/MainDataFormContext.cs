using ChronoFlow.Client.Common.Controls.Forms;

namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;

public sealed record MainDataFormContext<TViewModel>(TViewModel Item, FormContext FormContext)
    where TViewModel : class;
