using ChronoFlow.Server.Common.Messaging;
using ChronoFlow.Shared.Common.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ChronoFlow.Server.Common.Migrations.UseCases;

internal static class GetPendingMigrations
{
    internal static IEndpointRouteBuilder UseGetPendingMigrationsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/migrations/get-pending", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.SendAsync(new GetPendingMigrationsQuery());
            return Results.Ok(result);
        });

        return endpoints;
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
