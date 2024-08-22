using ChronoFlow.Client.Common.Processing.Search;

namespace ChronoFlow.Client.Common.MainData.Entities;

public abstract class MainDataViewModel : IMainDataViewModel
{
    [IgnoreOnSearch]
    public Guid Id { get; init; } = Guid.NewGuid();

    [IgnoreOnSearch]
    public DateTime Created { get; set; } = DateTime.Now;

    [IgnoreOnSearch]
    public DateTime LastChanged { get; set; } = DateTime.Now;
}
