namespace ChronoFlow.Server.Common.Persistence;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken = default);
}
