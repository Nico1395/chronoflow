namespace ChronoFlow.Server.Common.Migrations;

internal interface IMigrationRunner
{
    public Task MigrateAsync(CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<string>> GetAppliedMigrationsAsync(CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<string>> GetPendingMigrationsAsync(CancellationToken cancellationToken = default);
}
