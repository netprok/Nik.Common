namespace Nik.Common;

public class EnvironmentHelper : IEnvironmentHelper
{
    private const string DotnetVariable = "DOTNET_ENVIRONMENT";
    private const string AspNetCoreVariable = "ASPNETCORE_ENVIRONMENT";

    private const string Development = "Development";
    private const string Staging = "Staging";
    private const string Production = "Production";

    private const string AppSettingsFile = "appsettings.json";

    private string[] ValidEnvironments = new string[] { Development, Staging, Production };
    private string activeEnvironment = string.Empty;

    public IConfigurationRoot CreateConfiguration()
    {
        var environment = GetEnvironmentName();
        Environment.SetEnvironmentVariable(DotnetVariable, environment);
        Environment.SetEnvironmentVariable(AspNetCoreVariable, environment);

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(AppSettingsFile)
            .AddJsonFile(GetEnvironmentFile(environment))
            .Build();

        return configuration;
    }

    private static string GetEnvironmentFile(string environment)
    {
        return $"appsettings.{environment}.json";
    }

    public string GetEnvironmentName()
    {
        if (string.IsNullOrWhiteSpace(activeEnvironment))
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile(AppSettingsFile)
               .Build();

            var tempName = configuration.GetValue<string>("EnvironmentName")!;
            if (string.IsNullOrWhiteSpace(tempName))
            {
                tempName = Development;
            }
            if (!ValidEnvironments.Contains(tempName))
            {
                throw new Exception($"Unknown environment name: {tempName}");
            }
            activeEnvironment = tempName;

            DeleteOtherSettingsFiles();
        }

        return activeEnvironment;
    }

    private void DeleteOtherSettingsFiles()
    {
        IEnumerable<string> otherFiles = ValidEnvironments
            .Except(new string[] { activeEnvironment })
            .Select(env => GetEnvironmentFile(env));

        foreach (var file in otherFiles)
        {
            if (File.Exists(file))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                }
            }
        }
    }

    public bool IsProduction() => GetEnvironmentName().ToLower() == "production";

    public bool IsStaging() => GetEnvironmentName().ToLower() == "staging";

    public bool IsDevelopment() => GetEnvironmentName().ToLower() == "development";
}