using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace ChronoFlow.Server.Common.Controllers;

internal sealed class ControllerApplicationFeatureProvider(ControllerOptions options) : IApplicationFeatureProvider<ControllerFeature>
{
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        var controllerTypeInfos = ResolveControllerTypeInfos();
        foreach (var controllerTypeInfo in controllerTypeInfos)
            feature.Controllers.Add(controllerTypeInfo);
    }

    private List<TypeInfo> ResolveControllerTypeInfos()
    {
        return ReflectControllerTypes().Select(t => t.GetTypeInfo()).ToList();
    }

    private IEnumerable<Type> ReflectControllerTypes()
    {
        return options.Assemblies.SelectMany(assembly => assembly.GetTypes()).Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(ControllerBase)));
    }
}
