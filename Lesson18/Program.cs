List<int> source = new List<int> {5, 8, 4, 2, 7, 3, 7, 8};
List<int> other = new List<int> {3, 5, 7, 2, 5, 7};
var res = source.Where(i => i > 1 && i < 7).ToList();
var ints = source.OfType<int>().ToList();
var doubles = source.OfType<double>().ToList();
var skipTwoTakeThree = source.Skip(2).Take(3).ToList();
var skipTake = source.SkipWhile(e => e < 8).ToList();
skipTake = skipTake.TakeWhile(e => e > 2).ToList();
var dist = other.Distinct().ToList();
var source2 = source;
source2.AddRange(other);
var union2 = source2.Distinct().ToList();
var union = source.Union(other).ToList();
var except = source.Except(other).ToList();
var intersect = source.Intersect(other).ToList();
List<User> users = new List<User>
{
    new(3, "John", "some info"),
    new(1, "Mary", "other info"),
    new(5, "Patrick", "text"),
    new(2, "Jane", "many many many text")
};
var namesAbc = users
    // .OrderBy(u => u.Name)
    .Select(u => u.Name)
    .OrderBy(n => n)
    .ToList();
// return list of users ordered by 1.name 2.id
var usersOrd = users
    .OrderBy(u => u.Name)
    .ThenBy(u => u.Id)
    .ToList();
source.Reverse();
// Write a LINQ query that returns a list
// of all users whose Name starts with the letter 'A'.
var aUsers = users.Where(u => u.Name.ToLower()
    .StartsWith('a')).ToList();
// Write a LINQ query that returns the user with the highest Id.
var maxId = users.Max(u => u.Id); // max id, not the user
var maxIdUser = users.MaxBy(u => u.Id); // user
// Write a LINQ query that returns the top 3 users
// with the longest Info strings.
var infoUsers = users
    .OrderByDescending(u => u.Info.Length).Take(3).ToList();
// Write a LINQ query that returns a list of user
// Ids and Names, ordered by Name alphabetically.
var idNames = users
    .Select(u => new ValueTuple<int, string>(u.Id, u.Name))
    .OrderBy(t => t.Item2).ToList();
// Write a LINQ query that groups users by the first
// letter of their Name, and returns the count of users in each group.
var gr = users.GroupBy(u => u.Name.ToLower()[0])
    .Select(gr =>
        new ValueTuple<char, int>(gr.Key, gr.Count())).ToList();
// Write a LINQ query that returns all users
// whose Info property contains the word "admin".
var admins = users.Where(u => u.Info.Contains("admin")).ToList();
//Write a LINQ query that calculates the average length
//of the Name property for all users in the list.
var avgName = (int)users.Average(u => u.Name.Length);
// Write a LINQ query that returns users with
// unique Names, removing any duplicates.
var distinctNameUsers = users.DistinctBy(u => u.Name).ToList();
// Write a LINQ query that selects users with an odd Id value and orders
// the result by the length of their Info property in descending order.
var oddByInfo = users.Where(u => u.Id % 2 == 1)
    .OrderByDescending(u => u.Info.Length).ToList();
// Write a LINQ query that retrieves the top 5 users with the
// shortest Names, and returns their Id and Name properties
// in a new anonymous object.
var resUsers = users.OrderBy(u => u.Name.Length)
    .Take(5)
    .Select(u => new ValueTuple<int, string>(u.Id, u.Name)).ToList();
Console.ReadLine();


public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Info { get; set; }

    public User(int id, string name, string info)
    {
        Id = id;
        Name = name;
        Info = info;
    }
}