using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.Common.Migrations;

internal sealed class MigrationRunner(DbContext _dbContext) : IMigrationRunner
{
    public async Task<IReadOnlyList<string>> GetAppliedMigrationsAsync(CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Database.GetAppliedMigrationsAsync(cancellationToken)).ToList();
    }

    public async Task<IReadOnlyList<string>> GetPendingMigrationsAsync(CancellationToken cancellationToken = default)
    {
        return (await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).ToList();
    }

    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.MigrateAsync(cancellationToken);
    }
}
