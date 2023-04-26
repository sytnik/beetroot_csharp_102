using System.Text.Json;

namespace Lesson24;

public static class TextJsonSerialization
{
    public static void SerializePersons(this IEnumerable<Person> persons)
    {
        var settings = new JsonSerializerOptions {WriteIndented = true};
        var serialized = JsonSerializer.Serialize(persons,
            new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText("persons-text.json", serialized);
    }

    public static void SerializePersonsOne(this IEnumerable<Person> persons) =>
        File.WriteAllText("persons-text.json", JsonSerializer.Serialize(persons));

    public static async Task SerializePersonsAsync(this IEnumerable<Person> persons)
    {
        // async overload JsonSerializer.Serialize
        //  JsonSerializer.SerializeAsync
        string text;
        using (MemoryStream stream = new MemoryStream())
        {
            await JsonSerializer.SerializeAsync(stream, persons);
            long pos = stream.Position;
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                text = await reader.ReadToEndAsync();
            }
        }

        await File.WriteAllTextAsync("persons-text.json", text);
    }

    public static IEnumerable<Person> DeserializePersons()
    {
        string personsData = File.ReadAllText("persons-text.json");
        return JsonSerializer.Deserialize<IEnumerable<Person>>(personsData);
    }

    public static IEnumerable<Person> DeserializePersonsOne()
        => JsonSerializer.Deserialize<IEnumerable<Person>>
            (File.ReadAllText("persons-text.json"));
}