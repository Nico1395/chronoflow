using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace ChronoFlow.Server.Common.Migrations.UseCases;

internal static class MigrateMigrations
{
    [ApiController]
    public sealed class MigrationsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("api/migrations/migrate")]
        public async Task<ActionResult<Result>> MigrateMigrationsAsync()
        {
            var result = await _mediator.SendAsync(new MigrateMigrationsCommand());
            return Ok(result);
        }
    }

    private sealed record MigrateMigrationsCommand() : ICommand<Result>;

    private sealed class MigrateMigrationsCommandHandler(IMigrationRunner _migrationRunner) : ICommandHandler<MigrateMigrationsCommand, Result>
    {
        public async Task<Result> Handle(MigrateMigrationsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _migrationRunner.MigrateAsync(cancellationToken);
                return Result.Okay();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.ToString());
            }
        }
    }
}
