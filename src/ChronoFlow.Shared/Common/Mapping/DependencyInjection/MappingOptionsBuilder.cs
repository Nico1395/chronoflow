using ChronoFlow.Shared.Common.Mapping.Configuration;
using System.Reflection;

namespace ChronoFlow.Shared.Common.Mapping.DependencyInjection;

public sealed class MappingOptionsBuilder
{
    private readonly MappingOptions _options = new();

    public MappingOptionsBuilder AddProfile<TProfile>()
        where TProfile : class, IMappingProfile
    {
        return AddProfile(typeof(TProfile));
    }

    public MappingOptionsBuilder AddProfile(Type profileType)
    {
        if (profileType.IsAbstract || profileType.IsInterface || !profileType.IsAssignableTo(typeof(IMappingProfile)))
            throw new ArgumentException($"The type '{profileType.Name}' is not a non-abstract implementation of the '{typeof(IMappingProfile)}' interface.");

        _options.ProfileTypes.Add(profileType);
        return this;
    }

    public MappingOptionsBuilder ScanForProfilesInAssemblies(params Assembly[] assemblies)
    {
        var profileTypes = assemblies.SelectMany(a => a.GetTypes()).Where(t => (!t.IsAbstract || !t.IsInterface) && t.IsAssignableTo(typeof(IMappingProfile)));
        foreach (var profileType in profileTypes)
            _options.ProfileTypes.Add(profileType);

        return this;
    }

    internal MappingOptions Build()
    {
        return _options;
    }
}
