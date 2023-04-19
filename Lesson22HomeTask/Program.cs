using Lesson22Lib;
using Newtonsoft.Json;

namespace Lesson22HomeTask;

internal static class Program
{
    private static async Task Main()
    {
        // indentation
        
        
        var persons = 
            JsonConvert.DeserializeObject<IEnumerable<Person>>(File.ReadAllText("data.json"));
        // TextJsonSerialization.SerializePersons(persons);
        // await TextJsonSerialization.SerializePersonsAsync(persons);
        // var persons2 = TextJsonSerialization.DeserializePersons().ToList();
        // persons.SerializePersons();
        // persons.SerializePersonsToXml();
        // List<Person> persons2 = XmlSerialization.DeserializePersonsFromXml().ToList();
        NpoiTest.PublishExcel();
    }
}