namespace Nik.Common.Abstractions;

public interface IEnvironmentHelper
{
    IConfigurationRoot CreateConfiguration(IServiceCollection services, params string[] additionalFiles);

    IConfigurationRoot CreateConfiguration(params string[] additionalFiles);

    string GetEnvironmentName();

    bool IsDevelopment();

    bool IsProduction();

    bool IsStaging();
}