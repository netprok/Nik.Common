namespace Nik.Common;

public class ObjectMapper : IObjectMapper
{
    public TDestination Map<TDestination>(object source)
        where TDestination : new()
    {
        var destination = new TDestination();

        Map<TDestination>(source, ref destination);

        return destination;
    }

    public void Map<TDestination>(object source, ref TDestination destination) where TDestination : new()
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

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
    }

    private bool IsPrimitiveType(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string);
    }

    private PropertyInfo? FindDestinationProperty(PropertyInfo sourceProperty, PropertyInfo[] destinationProperties)
    {
        Type[] enumValidTypeNames = [typeof(int), typeof(uint), typeof(short), typeof(ushort), typeof(string)];

        return destinationProperties.FirstOrDefault(D =>
            D.Name.ToLower() == sourceProperty.Name.ToLower() &&
            (
                (D.PropertyType.FullName?.Contains(sourceProperty.PropertyType.FullName ?? string.Empty) ?? false) ||
                (sourceProperty.PropertyType.FullName?.Contains(D.PropertyType.FullName ?? string.Empty) ?? false) ||
                (D.PropertyType.IsEnum && enumValidTypeNames.Contains(sourceProperty.PropertyType)) ||
                (sourceProperty.PropertyType.IsEnum && enumValidTypeNames.Contains(D.PropertyType))
            )
        );
    }
}