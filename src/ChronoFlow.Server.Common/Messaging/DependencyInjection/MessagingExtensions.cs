using ChronoFlow.Server.Common.Messaging.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Server.Common.Messaging.DependencyInjection;

internal static class MessagingExtensions
{
    internal static IServiceCollection AddMessaging(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assemblies);
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

        services.AddScoped<IMediator, Mediator>();

        return services;
    }
}
