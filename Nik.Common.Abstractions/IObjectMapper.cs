using Nik.Common;
namespace Nik.Common.Abstractions
{
    public interface IObjectMapper
    {
        TDestination Map<TDestination>(object source) where TDestination : new();
    }
}
