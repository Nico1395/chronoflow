using Force.DeepCloner;

namespace ChronoFlow.Shared.Common.Cloning;

public static class CloningExtensions
{
    public static T CloneInstance<T>(this T @object)
    {
        return @object.DeepClone();
    }
}
