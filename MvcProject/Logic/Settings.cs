using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lesson36.Logic;

public static class Settings
{
    public static JsonSerializerOptions SerializerOptions = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };
}