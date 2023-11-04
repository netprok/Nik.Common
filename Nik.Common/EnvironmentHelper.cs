namespace Nik.Common;

public class EnvironmentHelper : IEnvironmentHelper
{
    private string[] ValidEnvironmentNamess = new string[] { "Development", "Staging", "Production" };
    private string environmentName = string.Empty;

    public IConfigurationRoot CreateConfiguration()
    {
        var environment = GetEnvironmentName();
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", environment);

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

            environmentName = configuration.GetValue<string>("EnvironmentName")!;
            if (!ValidEnvironmentNamess.Contains(environmentName))
            {
                throw new Exception($"Unknown environment name: {environmentName}");
            }
        }

        return environmentName;
    }

    public bool IsProduction() => GetEnvironmentName().ToLower() == "production";

    public bool IsStaging() => GetEnvironmentName().ToLower() == "staging";

    public bool IsDevelopment() => GetEnvironmentName().ToLower() == "development";
}