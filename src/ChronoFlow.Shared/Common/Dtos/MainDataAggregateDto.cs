namespace ChronoFlow.Shared.Common.Dtos;

public abstract class MainDataAggregateDto
{
    public Guid Id { get; init; }
    public DateTime TimestampCreated { get; init; }
    public DateTime TimestampLastChanged { get; init; }
    public Guid? LastChangedUserId { get; init; }
}
