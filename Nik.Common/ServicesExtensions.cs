namespace Nik.Common;

public static class ServicesExtensions
{
    public static IServiceCollection AddNikCommon(this IServiceCollection services)
    {
        services.AddSingleton<IEnvironmentHelper, EnvironmentHelper>();
        services.AddSingleton<IStringFormatter, EnglishStringFormatter>();
        services.AddSingleton<IHashGenerator, SHA256HashGenerator>();
        services.AddSingleton<IObjectMapper, ObjectMapper>();
        services.AddSingleton<IListSplitter, ListSplitter>();
        services.AddSingleton<IAesEncryptor, AesEncryptor>();

        return services;
    }
}