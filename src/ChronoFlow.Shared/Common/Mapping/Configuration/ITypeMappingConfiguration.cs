namespace ChronoFlow.Shared.Common.Mapping.Configuration;

public interface ITypeMappingConfiguration
{
    public Type SourceType { get; }
    public Type DestinationType { get; }
}
