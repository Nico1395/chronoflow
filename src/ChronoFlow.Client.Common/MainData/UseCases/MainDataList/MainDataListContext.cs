namespace ChronoFlow.Client.Common.MainData.UseCases.MainDataList;

public sealed record MainDataListContext<TViewModel>(List<TViewModel> Items, bool Busy)
    where TViewModel : class;
