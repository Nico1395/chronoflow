using ChronoFlow.Server.Common.Messaging.Behaviors;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChronoFlow.Server.Common.Messaging.DependencyInjection;

internal static class MessagingExtensions
{
    internal static IServiceCollection AddMessaging(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assemblies);
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(GlobalExceptionHandler<,,>));
        services.AddScoped<IMediator, Mediator>();

        return services;
    }
}
