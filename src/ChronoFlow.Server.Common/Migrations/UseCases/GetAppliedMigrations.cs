using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.Common.Migrations.UseCases;

internal static class GetAppliedMigrations
{
    [ApiController]
    public sealed class MigrationsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("api/migrations/get-applied")]
        public async Task<ActionResult<Result<IReadOnlyList<string>>>> GetAppliedMigrationsAsync()
        {
            var result = await mediator.SendAsync(new GetAppliedMigrationsQuery());
            return Ok(result);
        }
    }

    private sealed record GetAppliedMigrationsQuery : IQuery<Result<IReadOnlyList<string>>>;

    private sealed class GetAppliedMigrationsQueryHandler(IMigrationRunner _migrationRunner) : IQueryHandler<GetAppliedMigrationsQuery, Result<IReadOnlyList<string>>>
    {
        public async Task<Result<IReadOnlyList<string>>> Handle(GetAppliedMigrationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appliedMigrations = await _migrationRunner.GetAppliedMigrationsAsync(cancellationToken);
                return Result.Okay(appliedMigrations);
            }
            catch (Exception ex)
            {
                return Result.Error<IReadOnlyList<string>>(ex.ToString());
            }
        }
    }
}
