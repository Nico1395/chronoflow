namespace ChronoFlow.Server.Common.Configuration;

internal sealed class EnvironmentConfigurationProvider : IConfigurationProvider
{
    public string GetConnectionString()
    {
        return Environment.GetEnvironmentVariable(ConfigurationConstants.EnvironmentVariables.ConnectionString)
            ?? throw new ArgumentException($"No connection string specified! Specify the variable {ConfigurationConstants.EnvironmentVariables.ConnectionString} with a valid connection string.");
    }
}
