using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.Common.Migrations.UseCases;

internal static class GetAppliedMigrations
{
    internal static IEndpointRouteBuilder UseGetAppliedMigrationsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/migrations/get-applied", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.SendAsync(new GetAppliedMigrationsQuery());
            return Results.Ok(result);
        });

        return endpoints;
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
