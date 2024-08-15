using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Server.Common.Persistence.Context;

public static class DbContextModelOptionsExtensions
{
    public static IServiceCollection AddDbContextModelOptions<TDbContext>(this IServiceCollection services, Action<DbContextModelOptions<TDbContext>> optionsAction)
        where TDbContext : DbContext
    {
        var modelOptions = new DbContextModelOptions<TDbContext>();
        optionsAction.Invoke(modelOptions);

        return services.AddSingleton(sp => modelOptions);
    }
}
