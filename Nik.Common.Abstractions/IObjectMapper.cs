namespace Nik.Common.Abstractions;

public interface IObjectMapper
{
    TDestination Map<TDestination>(object source) where TDestination : new();

    void Map<TDestination>(object source, ref TDestination destination) where TDestination : new();
}