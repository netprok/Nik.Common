namespace Nik.Common.Abstractions;

public interface IObjectMapper
{
    TDestination Map<TDestination>(object source) where TDestination : new();

    void MapUpdate<TDestination>(object source, ref TDestination destination) where TDestination : new();
}