namespace Nik.Common.Abstractions;

public interface IEnvironmentHelper
{
    IConfigurationRoot CreateConfiguration(IServiceCollection? services = null, params string[] additionalFiles);

    string GetEnvironmentName();

    bool IsDevelopment();

    bool IsProduction();

    bool IsStaging();
}