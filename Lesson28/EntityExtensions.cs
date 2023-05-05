namespace Lesson28;

public static class EntityExtensions
{
    public static T TrimStringProps<T>(this T entity) where T : class
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
            if (property.PropertyType == typeof(string) && property.SetMethod != null)
                if (property.GetValue(entity) is string originalValue)
                    property.SetValue(entity, originalValue.Trim());
        return entity;
    }
}