using Microsoft.Extensions.Configuration;

namespace ChronoFlow.Server.Common.Configuration;

internal sealed class AppsettingsConfigurationProvider(IConfiguration _configuration) : IConfigurationProvider
{
    public string GetConnectionString()
    {
        return _configuration.GetConnectionString(ConfigurationConstants.AppsettingsSections.ConnectionString)
            ?? throw new ArgumentException($"No connection string specified! Add a connection string under 'ConnectionStrings' -> '{ConfigurationConstants.AppsettingsSections.ConnectionString}'.");
    }
}
