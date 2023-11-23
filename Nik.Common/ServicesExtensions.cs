namespace Nik.Common;

public static class ServicesExtensions
{
    public static IServiceCollection UseEnvironmentHelper(this IServiceCollection services)
    {
        services.AddSingleton<IEnvironmentHelper, EnvironmentHelper>();

        IEnvironmentHelper environmentHelper = new EnvironmentHelper();
        var configuration = environmentHelper.CreateConfiguration();
        services.AddSingleton(configuration);

        return services;
    }

public static IServiceCollection UseCommon(this IServiceCollection services)
    {
        services.AddSingleton<IStringFormatter, EnglishStringFormatter>();
        services.AddSingleton<IHashGenerator, SHA256HashGenerator>();
        services.AddSingleton<IObjectMapper, ObjectMapper>();

        return services;
    }

    public static IServiceCollection UseAesEncryption(this IServiceCollection services)
    {
        services.AddSingleton<IEncryptor, AESEncryptor>();

        return services;
    }
}