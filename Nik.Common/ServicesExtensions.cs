namespace Nik.Common;

public static class ServicesExtensions
{
    public static IServiceCollection AddNikCommon(this IServiceCollection services)
    {
        services.AddSingleton<IStringFormatter, EnglishStringFormatter>();
        services.AddSingleton<IObjectMapper, ObjectMapper>();
        services.AddSingleton<IListSplitter, ListSplitter>();

        return services;
    }
}