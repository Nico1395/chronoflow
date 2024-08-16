using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.Common.Persistence;

internal sealed class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
