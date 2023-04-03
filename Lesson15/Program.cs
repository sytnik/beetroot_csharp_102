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
Console.ReadLine();
// void SwapInts(ref int i, ref int i1)
// {
//     int temp = i;
//     i = i1;
//     i1 = temp;
// }

// void SwapPersons(Person person3, Person person4)
// {
//     Person temp1 = person3;
//     person3 = person4;
//     person4 = temp1;
// }