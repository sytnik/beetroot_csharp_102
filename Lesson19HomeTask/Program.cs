using Newtonsoft.Json;

namespace Lesson19HomeTask;

internal static class Program
{
    // todo the following tasks
    // 1. find out who is located farthest north/south/west/east using latitude/longitude data
    // 2. find max and min distance between 2 persons
    // 3. find 2 persons whos ‘about’ have the most same words
    // 4. find persons with same friends (compare by friend’s name)
    
    private static void Main()
    {
        var persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(File.ReadAllText("data.json"));
    }
}