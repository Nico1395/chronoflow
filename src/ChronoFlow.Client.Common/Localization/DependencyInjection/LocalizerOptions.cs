namespace ChronoFlow.Client.Common.Localization.DependencyInjection;

public class LocalizerOptions
{
    internal LocalizerOptions()
    {
    }

    private readonly List<Type> _resourceTypes = [];

    public LocalizerOptions AddResource<TResource>()
    {
        return AddResource(typeof(TResource));
    }

    public LocalizerOptions AddResource(Type resourceType)
    {
        _resourceTypes.Add(resourceType);
        return this;
    }

    internal IReadOnlyList<Type> GetResourceTypes()
    {
        return _resourceTypes.AsReadOnly();
    }
}
