namespace Nik.Common;

public static class ServicesExtensions
{
    public static IServiceCollection UseCommon(this IServiceCollection services)
    {
        services.AddSingleton<IStringFormatter, EnglishStringFormatter>();
        services.AddSingleton<IHashGenerator, SHA256HashGenerator>();
        services.AddSingleton<IEnvironmentHelper, EnvironmentHelper>();

        return services;
    }
}