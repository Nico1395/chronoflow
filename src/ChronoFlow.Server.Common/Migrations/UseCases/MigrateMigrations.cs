using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.Common.Migrations.UseCases;

internal static class MigrateMigrations
{
    internal static IEndpointRouteBuilder UseMigrateMigrationsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/migrations/migrate", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.SendAsync(new MigrateMigrationsCommand());
            return Results.Ok(result);
        });

        return endpoints;
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
