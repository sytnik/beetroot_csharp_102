using System.Text.Json;
using System.Text.RegularExpressions;

namespace Lesson8;

public class Program
{
    public static void Main(string[] args)
    {
        WorkWintFile();
      
        Console.WriteLine("Hello, World!");
        // RegexTest();
        // Person person - is object of class (of type) Person
        // string name = Console.ReadLine();
        // int age = Convert.ToInt32(Console.ReadLine());
        // Person person = new Person(name, age);
        // person.PrintInfo();
        Person[] persons = new[]
        {
            new Person("user1", 25),
            new Person("user2", 30),
            new Person("user3", 35)
        };
        Serialize(persons);
        Person[] personsFromFile = Deserialize();
    }

    public static void Serialize(Person[] persons)
    {
        using (var fileStream = 
               new FileStream("Person.json",
                   FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fileStream, persons);
        }
    }

    public static Person[] Deserialize()
    {
        using (var fileStream =
               new FileStream("Person.json",
                   FileMode.OpenOrCreate))
        {
            return JsonSerializer // returns Person[]
                .Deserialize<Person[]>(fileStream);
        }
    }
    private static void WorkWintFile()
    {
        string[] users = File.ReadAllLines("Text.txt");
        string newUser = "User3 0971236547";
        File.AppendAllText("Text.txt",
            Environment.NewLine);
        File.AppendAllText("Text.txt", newUser);
        string[] newUserArr = new[] { newUser };
        File.AppendAllText("Text.txt", "\r\n");
        File.AppendAllLines("Text.txt", newUserArr);
        string filePath = "Text1.txt";
        File.Delete(filePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Console.WriteLine("The file has been deleted.");
        }
        else
        {
            Console.WriteLine("The file does not exist.");
        }
    }

    // клас Person (public = доступ без обмежень)
    public class Person
    {
        // поле (field) класу
        public string Name { get; set; }
        // властивість (property) класу
        public int Age { get; set; }

        // конструктор (constructor) класу
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // метод (method) класу
        public void PrintInfo() => Console.WriteLine($"Name: {Name}, Age: {Age}");
    }

    public static void RegexTest()
    {
        string input = "The quick brown fox jumps over the lazy dog.";
        string pattern = "fox";
        Match match = Regex.Match(input, pattern);
        if (match.Success)
        {
            Console.WriteLine("Found '{0}' at position {1}", match.Value, match.Index);
        }
        input = "The quick brown fox jumps over the lazy dog.";
        pattern = @"\b\w{4}\b"; // word with 4 letters
        MatchCollection matches = Regex.Matches(input, pattern);
        foreach (Match match1 in matches)
        {
            Console.WriteLine($"Found '{match1.Value}' at position {match1.Index}");
        }

        input = "The quick brown fox jumps over the lazy dog.";
        pattern = @"\s";
        string replacement = "_";
        string result = Regex.Replace(input, pattern, replacement);
        Console.WriteLine(result);

        input = "The quick brown fox jumps over the lazy dog.";
        pattern = "fox";
        bool isMatch = Regex.IsMatch(input, pattern);
        Console.WriteLine(isMatch);

        pattern = @"\+38\(0\d{2}\)\d{7}"; // +38(0xx)xxxxxxx
        input = "+38(099)1234567";
        Regex regex = new Regex(pattern);
        isMatch = regex.IsMatch(input); // true

        pattern = @"^[А-ЩЬЮЯҐЄІЇ][а-щьюяґєії']+\s[А-ЩЬЮЯҐЄІЇ]\.[А-ЩЬЮЯҐЄІЇ]\.$"; // ^ - start of the string
        regex = new Regex(pattern);

        input = "Іванов І.П.";
        isMatch = regex.IsMatch(input); // true

        string email = "example@example.com";
        pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        regex = new Regex(pattern);

        isMatch = regex.IsMatch(email);
        Console.WriteLine(isMatch); // виведе "True”


    }
}



public class Methods
{

}