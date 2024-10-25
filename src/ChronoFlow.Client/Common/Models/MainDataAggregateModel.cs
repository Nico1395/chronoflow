namespace ChronoFlow.Client.Common.Models;

public abstract class MainDataAggregateModel
{
    public Guid Id { get; init; }
    public DateTime TimestampCreated { get; init; }
    public DateTime TimestampLastChanged { get; init; }
    public Guid? LastChangedUserId { get; init; }
}
