using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.Common.Migrations.UseCases;

internal static class GetPendingMigrations
{
    [ApiController]
    public sealed class MigrationsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("api/migrations/get-pending")]
        public async Task<ActionResult<Result<IReadOnlyList<string>>>> GetPendingMigrationsAsync()
        {
            var result = await _mediator.SendAsync(new GetPendingMigrationsQuery());
            return Ok(result);
        }
    }

    private sealed record GetPendingMigrationsQuery() : IQuery<Result<IReadOnlyList<string>>>;

    private sealed class GetPendingMigrationsQueryHandler(IMigrationRunner _migrationRunner) : IQueryHandler<GetPendingMigrationsQuery, Result<IReadOnlyList<string>>>
    {
        public async Task<Result<IReadOnlyList<string>>> Handle(GetPendingMigrationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pendingMigrations = await _migrationRunner.GetPendingMigrationsAsync(cancellationToken);
                return Result.Okay(pendingMigrations);
            }
            catch (Exception ex)
            {
                return Result.Error<IReadOnlyList<string>>(ex.ToString());
            }
        }
    }
}
