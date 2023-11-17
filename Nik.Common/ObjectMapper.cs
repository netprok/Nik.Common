using Nik.Common.Abstractions;

namespace Nik.Common;

public class ObjectMapper : IObjectMapper
{
    public TDestination Map<TDestination>(object source)
        where TDestination : new()
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var destination = new TDestination();

        var sourceProperties = source.GetType().GetProperties();
        var destinationProperties = typeof(TDestination).GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = FindDestinationProperty(sourceProperty, destinationProperties);

            if (destinationProperty != null && destinationProperty.CanWrite)
            {
                var value = sourceProperty.GetValue(source);

                if (value != null && !IsPrimitiveType(sourceProperty.PropertyType))
                {
                    value = typeof(ObjectMapper).GetMethod(nameof(ObjectMapper.Map))!.MakeGenericMethod(destinationProperty.PropertyType).Invoke(null, new[] { value });
                }

                destinationProperty.SetValue(destination, value);
            }
        }

        return destination;
    }

    private bool IsPrimitiveType(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string);
    }

    private PropertyInfo? FindDestinationProperty(PropertyInfo sourceProperty, PropertyInfo[] destinationProperties)
    {
        return destinationProperties.FirstOrDefault(D =>
            D.Name.ToLower() == sourceProperty.Name.ToLower() &&
            (
                (D.PropertyType.FullName?.Contains(sourceProperty.PropertyType.FullName ?? string.Empty) ?? false) ||
                (sourceProperty.PropertyType.FullName?.Contains(D.PropertyType.FullName ?? string.Empty) ?? false)
            )
        );
    }
}