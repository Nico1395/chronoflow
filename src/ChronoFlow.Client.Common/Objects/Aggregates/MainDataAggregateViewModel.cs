using ChronoFlow.Client.Common.Processing.Search;

namespace ChronoFlow.Client.Common.Objects.Aggregates;

public abstract class MainDataAggregateViewModel
{
    [IgnoreOnSearch]
    public Guid Id { get; init; } = Guid.NewGuid();

    [IgnoreOnSearch]
    public DateTime Created { get; set; } = DateTime.Now;

    [IgnoreOnSearch]
    public DateTime LastChanged { get; set; } = DateTime.Now;
}
