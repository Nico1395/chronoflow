namespace ChronoFlow.Shared.Common.Mapping.Configuration;

internal sealed class TypeMappingConfiguration : ITypeMappingConfiguration
{
    public required Type SourceType { get; init; }
    public required Type DestinationType { get; init; }
}
