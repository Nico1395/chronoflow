using ChronoFlow.Client.Common.MainData.Entities;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm;
using ChronoFlow.Client.Common.MainData.UseCases.MainDataForm.Controller;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoFlow.Client.Common.MainData;

public static class DependencyInjection
{
    public static IServiceCollection AddMainDataFormService<TViewModel>(this IServiceCollection services)
        where TViewModel : class, IMainDataViewModel
    {
        return services.AddScoped<IMainDataFormService<TViewModel>, MainDataFormService<TViewModel>>();
    }

    public static IServiceCollection AddMainDataFormService<TViewModel, TInterface, TImplementation>(this IServiceCollection services)
        where TViewModel : class, IMainDataViewModel
        where TImplementation : class, TInterface, IMainDataFormService<TViewModel>
    {
        services.AddScoped<IMainDataFormService<TViewModel>, TImplementation>();
        services.AddScoped(typeof(TInterface), sp => (sp.GetRequiredService<IMainDataFormService<TViewModel>>() as TImplementation)!);

        return services;
    }

    public static IServiceCollection AddMainDataFormContext<TViewModel>(this IServiceCollection services)
        where TViewModel : class, IMainDataViewModel
    {
        return services.AddScoped<IMainDataFormController<TViewModel>, MainDataFormController<TViewModel>>();
    }
}
