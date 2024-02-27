namespace Nik.Common;

public class ObjectMapper : IObjectMapper
{
    public TDestination Map<TDestination>(object source, bool checkType)
        where TDestination : new()
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        TDestination destination = new TDestination();

        PropertyInfo[] sourceProperties = source.GetType().GetProperties();
        PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = GetDestinationProperty(destinationProperties, sourceProperty, checkType);

            if (destinationProperty != null && destinationProperty.CanWrite)
            {
                object value = sourceProperty.GetValue(source)!;

                if (value != null && !IsPrimitiveType(sourceProperty.PropertyType))
                {
                    if (value is IList list)
                    {
                        value = HandleList(destinationProperty, list, checkType);
                    }
                    else
                    {
                        value = HandleObject(destinationProperty, value, checkType);
                    }
                }

                destinationProperty.SetValue(destination, value);
            }
        }

        return destination;
    }

    private object HandleObject(PropertyInfo destinationProperty, object value, bool checkType)
    {
        value = GetMapMethod(destinationProperty.PropertyType)
            .Invoke(this, new[] { value, checkType })!;
        return value;
    }

    private object HandleList(PropertyInfo destinationProperty, IList list, bool checkType)
    {
        object value;
        Type destinationListType = destinationProperty.PropertyType;
        IList destinationList = (IList)Activator.CreateInstance(destinationListType)!;

        foreach (var listItem in list)
        {
            object mappedListItem = GetMapMethod(destinationListType.GetGenericArguments()[0])
                .Invoke(this, new[] { listItem, checkType })!;

            destinationList.Add(mappedListItem);
        }

        value = destinationList;
        return value;
    }

    private static MethodInfo GetMapMethod(params Type[] typeArguments)
    {
        return typeof(ObjectMapper).GetMethod(nameof(ObjectMapper.Map))!.MakeGenericMethod(typeArguments);
    }

    private PropertyInfo? GetDestinationProperty(PropertyInfo[] destinationProperties, PropertyInfo sourceProperty, bool checkType)
    {
        if (!checkType)
        {
            return destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name)!;
        }
        Type[] enumValidTypeNames = [typeof(int), typeof(uint), typeof(short), typeof(ushort), typeof(string)];

        return destinationProperties.FirstOrDefault(destinationProperty =>
            destinationProperty.Name.ToLower() == sourceProperty.Name.ToLower() &&
            (
                (destinationProperty.PropertyType.FullName?.Contains(sourceProperty.PropertyType.FullName ?? string.Empty) ?? false) ||
                (sourceProperty.PropertyType.FullName?.Contains(destinationProperty.PropertyType.FullName ?? string.Empty) ?? false) ||
                (destinationProperty.PropertyType.IsEnum && enumValidTypeNames.Contains(sourceProperty.PropertyType)) ||
                (sourceProperty.PropertyType.IsEnum && enumValidTypeNames.Contains(destinationProperty.PropertyType)) ||
                (sourceProperty.PropertyType.IsClass && destinationProperty.PropertyType.IsClass)
            )
        );
    }

    private bool IsPrimitiveType(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string);
    }
}