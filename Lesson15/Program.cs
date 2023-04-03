using Lesson15Lib.Persons;
using Lesson15Lib.PhoneBook;

var person = new Family("0937649051", "Demianchuk", "Anton", 1);
var collegue = new Collegues("0508676944", "Melnichuk", "Yuliia", "Medical brain");
var person1 = new Person("0933597304", "semchuk", "oleksandr");
var person2 = new Person("0671234567", "Dmitriev", "Dmitrii");
var myPhoneBook = new PhoneBook(person, person1, person2, collegue);
var phoneBookSearcher = new PhoneBookSearcher("093", myPhoneBook);
foreach (var contact in phoneBookSearcher.SearchResult) Console.WriteLine($"\n{contact.GetFullInfo()}");
SaveDownloadPhoneBook.Serialize(myPhoneBook);
var phoneBook2 = SaveDownloadPhoneBook.Deserialize();
foreach(var contact in phoneBook2.Contacts) Console.WriteLine($"\n{contact.GetFullInfo()}");
Console.ReadLine();