namespace Nik.Common;

public class EnvironmentHelper : IEnvironmentHelper
{
    private const string DotnetVariableName = "DOTNET_ENVIRONMENT";
    private const string HostingVariableName = "ASPNETCORE_ENVIRONMENT";
    private string[] ValidEnvironmentNames = new string[] { "Development", "Staging", "Production" };
    private string environmentName = string.Empty;

    public IConfigurationRoot CreateConfiguration()
    {
        var environment = GetEnvironmentName();
        Environment.SetEnvironmentVariable(DotnetVariableName, environment);
        Environment.SetEnvironmentVariable(HostingVariableName, environment);

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json")
            .Build();

        return configuration;
    }

    public string GetEnvironmentName()
    {
        if (string.IsNullOrWhiteSpace(environmentName))
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            var tempName = configuration.GetValue<string>("EnvironmentName")!;
            environmentName = tempName;
            if (!ValidEnvironmentNames.Contains(tempName))
            {
                environmentName = "Development";
                throw new Exception($"Unknown environment name: {tempName}");
            }
        }

        return environmentName;
    }

    public bool IsProduction() => GetEnvironmentName().ToLower() == "production";

    public bool IsStaging() => GetEnvironmentName().ToLower() == "staging";

    public bool IsDevelopment() => GetEnvironmentName().ToLower() == "development";
}