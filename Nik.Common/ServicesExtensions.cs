namespace Nik.Common;

public static class ServicesExtensions
{
    public static IServiceCollection UseCommon(this IServiceCollection services)
    {
        services.AddSingleton<IEnvironmentHelper, EnvironmentHelper>();
        services.AddSingleton<IStringFormatter, EnglishStringFormatter>();
        services.AddSingleton<IHashGenerator, SHA256HashGenerator>();
        services.AddSingleton<IObjectMapper, ObjectMapper>();
        services.AddSingleton<IListSplitter, ListSplitter>();

        return services;
    }

    public static IServiceCollection UseAesEncryption(this IServiceCollection services)
    {
        services.AddSingleton<IEncryptor, AESEncryptor>();

        return services;
    }
}