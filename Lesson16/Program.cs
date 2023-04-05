using Lesson15Lib;
using Lesson15Lib.Persons;
using Lesson15Lib.PhoneBook;

// GenericStack<int> intStack = new GenericStack<int>();
// GenericStack<Person> persStack = new GenericStack<Person>();
// GenericStack<Collegue> collStack = new GenericStack<Collegue>();
// int[] dest = new int[10];
// intStack.Push(1);
// intStack.Push(2);
// intStack.Push(3);
// intStack.Push(4);
// intStack.Push(5);
// intStack.Peek();
// intStack.Pop();
// intStack.CopyTo(dest, 3);
// intStack.Clear();

var person = new Family("0937649051", "Demianchuk", "Anton", 1);
var collegue = new Collegue("0508676944", "Melnichuk", "Yuliia", "Medical brain");
var person1 = new Person("0933597304", "semchuk", "oleksandr");
var person2 = new Person("0671234567", "Dmitriev", "Dmitrii");
var myPhoneBook = new PhoneBook(person, person1, person2, collegue);
var phoneBookSearcher = new PhoneBookSearcher("093", myPhoneBook);
foreach (var contact in phoneBookSearcher.SearchResult) Console.WriteLine($"\n{contact.GetFullInfo()}");
SaveDownloadPhoneBook.Serialize(myPhoneBook);
var phoneBook2 = SaveDownloadPhoneBook.Deserialize();
foreach (var contact in phoneBook2.Contacts) Console.WriteLine($"\n{contact.GetFullInfo()}");
var text = File.ReadAllText(AppSetup.FilePath);
// swap int values
int a = 5, b = 10;
// SwapInts(ref a, ref b);
AppSetup.SwapGeneric(ref a, ref b);

// swap two persons
// SwapPersons(person1, person2);
AppSetup.SwapGeneric(ref person1, ref person2);


int fact = AppSetup.Factorial<int>(5);
double fact1 = AppSetup.Factorial(5.34);
List<SomeClass> list = new List<SomeClass>
{
    new SomeClass(1, "name1"),
    new SomeClass(2, "name2"),
    new SomeClass(3, "name3")
};
// list[0]._classId = 5;
// int classId = list[0]._classId;
PrintClassInfo(list);
PrintClassInfo(list.ToArray());
PrintClassInfo(list.ToHashSet());

Dictionary<string, SomeClass> dictionary = new Dictionary<string, SomeClass>();
SomeClass cls = new SomeClass(1, "name1");
dictionary.Add("key1", cls);
dictionary.Add("key2", new SomeClass(2, "name2"));
dictionary.Add("key3", new SomeClass(3, "name3"));
var keys = dictionary.Keys;
var values = dictionary.Values;
bool conKey = dictionary.ContainsKey("y1");
bool conVal = dictionary.ContainsValue(cls);
SomeClass val3 = dictionary["key3"];


int someClassId = 1;
string someClassName = "name";
SomeClass someClass = new SomeClass(someClassId, someClassName);

void PrintClassInfo(IEnumerable<SomeClass> someClassNum)
{
    // int count = someClassNum.Count();
    // for (int i = 0; i < count; i++)
    // {
    //     Console.WriteLine(someClassNum.ElementAt(i).PrintStr());
    // }
    foreach (var cl in someClassNum)
    {
        Console.WriteLine(cl.PrintStr());
    }
}

internal class SomeClass : ISomeClass
{
    private readonly int _classId = 1;
    private string ClassName { get; set; }

    internal SomeClass(int classId, string className)
    {
        _classId = classId;
        ClassName = className;
    }

    public string PrintStr() => $"Id: {_classId} - {ClassName}";
    public static string PrintStr(SomeClass someClass) =>
        $"Id: {someClass._classId} - {someClass.ClassName}";

    // public void SetClassId(int id) => _classId = id;
}

interface ISomeClass
{
    string PrintStr();
}