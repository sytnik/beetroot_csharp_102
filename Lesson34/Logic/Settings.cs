using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lesson34.Logic;

public static class Settings
{
    public static JsonSerializerOptions SerializerOptions = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };
}