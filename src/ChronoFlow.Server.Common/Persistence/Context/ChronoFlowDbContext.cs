using Microsoft.EntityFrameworkCore;

namespace ChronoFlow.Server.Common.Persistence.Context;

internal sealed class ChronoFlowDbContext(
    DbContextModelOptions<ChronoFlowDbContext> modelOptions,
    DbContextOptions<ChronoFlowDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromModelOptions(modelOptions);
    }
}
