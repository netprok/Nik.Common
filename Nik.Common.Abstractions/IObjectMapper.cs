namespace Nik.Common.Abstractions;

public interface IObjectMapper
{
    TDestination Map<TDestination>(object source, bool checkType = false) 
        where TDestination : new();
}