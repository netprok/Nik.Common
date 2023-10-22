namespace Nik.Common.Abstractions;

public interface IEnvironmentHelper
{
    IConfigurationRoot CreateConfiguration();

    string GetEnvironmentName();

    bool IsDevelopment();

    bool IsProduction();

    bool IsStaging();
}
