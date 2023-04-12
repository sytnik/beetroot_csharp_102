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
        // var joins = new Joins();
        // joins.JoinEntities();
        var persons = JsonConvert.DeserializeObject<IEnumerable<Person>>(File.ReadAllText("data.json"))
            .ToList();
        // Can you provide a LINQ query to find the average age of all people in the list?
        var age = persons.Average(p => p.Age);
        // Write a LINQ query to find the total number of male and female people in the list.
        var counts = persons
            .GroupBy(p => p.Gender)
            // .Select(gr => new {Gender = gr.Key, Count = gr.Count()})
            // .Select(gr => new ValueTuple<Gender, int>(gr.Key, gr.Count()))
            .Select(gr => new GrItem(gr.Key, gr.Count()))
            .ToList();
        // Create a method, How would you write a LINQ query to retrieve a list of
        // people who have a specific tag, such as "developer"?
        var persons2 = persons.GetPersonsByTag("Lorem");
        // 2. find max and min distance between 2 persons


        //user1 1-2 1-3 1-4
        //user2 2-3 2-4
        //user3 3-4
        //user4
        // var dist = persons.Distances();
        // var maxUsers = dist.MaxBy(d => d.Distance);
        // var minUsers = dist.MinBy(d => d.Distance);
        // persons.DistancesLight(out var min, out var max);
        // find 2 persons whos ‘about’ have the most same words
        persons.MaxAbout(out var max);
        //find first pair of persons with same friends (compare by friend’s name)

        // person -> (index, friendnamestr)
        var samefriends = persons
            .Select(p => new UserFriendsRecord(p.Index,
                string.Join("", p.Friends
                    .Select(f => f.Name)
                    .OrderBy(n => n))))
            .GroupBy(p => p.FriendStr)
            .Where(gr => gr.Count() > 1)
            .ToList();
    }

    public static List<UserDistanceRecord> Distances(this IEnumerable<Person> persons)
    {
        var personsList = persons.ToArray();
        List<UserDistanceRecord> records = new List<UserDistanceRecord>();
        for (int i = 0; i < personsList.Length - 1; i++)
        for (int j = i + 1; j < personsList.Length; j++)
            records.Add(new UserDistanceRecord(
                personsList[i].Index, personsList[j].Index,
                Distance(personsList[i].Latitude, personsList[i].Longitude,
                    personsList[j].Latitude, personsList[j].Longitude)));
        return records;
    }

    public static void DistancesLight(this IEnumerable<Person> persons,
        out UserDistanceRecord min, out UserDistanceRecord max)
    {
        min = new UserDistanceRecord(0, 0, 50000);
        max = new UserDistanceRecord(0, 0, 0);
        var personsList = persons.ToArray();
        for (int i = 0; i < personsList.Length - 1; i++)
        for (int j = i + 1; j < personsList.Length; j++)
        {
            var rec = new UserDistanceRecord(
                personsList[i].Index, personsList[j].Index,
                Distance(personsList[i].Latitude, personsList[i].Longitude,
                    personsList[j].Latitude, personsList[j].Longitude));
            if (rec.Distance < min.Distance) min = rec;
            else if (rec.Distance > max.Distance) max = rec;
        }
    }

    public static void MaxAbout(this IEnumerable<Person> persons,
        out UserAboutRecord max)
    {
        max = new UserAboutRecord(null, null, 0);
        var personsList = persons.ToArray();
        for (int i = 0; i < personsList.Length - 1; i++)
        for (int j = i + 1; j < personsList.Length; j++)
        {
            var first = personsList[i];
            var second = personsList[j];
            var firstWords = SplitAbout2(first);
            var secondWords = SplitAbout2(second);
            var common = firstWords.Intersect(secondWords).Count();
            if (common > max.WordsCount)
                max = new UserAboutRecord(first, second, common);

            var arr = new char[] {',', '.'};
            var sp = first.About.Split(arr);
        }
    }

    private static string[] SplitAbout(Person person)
    {
        return person.About.ToLower().Replace(".", "").Replace(",", "").Replace("\r\n", "").Split(" ");
    }

    private static string[] SplitAbout2(Person person)
    {
        return person.About.ToLower().Split('.', ',', ' ');
    }

    public record UserFriendsRecord(int Index, string FriendStr);

    public record UserDistanceRecord(int FirstIndex, int SecondIndex, double Distance);

    public record UserAboutRecord(Person first, Person second, int WordsCount);

    public static double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        double r = 6371; // Radius of the Earth in kilometers
        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = r * c;
        return distance;
    }

    public static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }

    public static List<Person> GetPersonsByTag(this IEnumerable<Person> persons, string tag)
    {
        return persons.Where(p => p.Tags.Contains(tag)).ToList();
    }

    //todo public static List<Person> GetPersonsByTags(this IEnumerable<Person> persons, params string[] tags)
    // {
    //     // return persons.Where(p => p.Tags.Contains(tag)).ToList();
    // }

    public record GrItem(Gender Gender, int Count);
}