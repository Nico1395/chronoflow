namespace ChronoFlow.Server.Common.Entities;

public abstract class MainDataAggregate
{
    public MainDataAggregate()
    {
        var now = DateTime.Now;

        TimestampCreated = now;
        TimestampLastChanged = now;
    }

    public required Guid Id { get; init; }
    public DateTime TimestampCreated { get; init; }
    public DateTime TimestampLastChanged { get; init; }
    public Guid? LastChangedUserId { get; init; }
}
