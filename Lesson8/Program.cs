using System.Text.RegularExpressions;

namespace Lesson8;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        RegexTest();
        // Person person - is object of class (of type) Person
        string name = Console.ReadLine();
        int age = Convert.ToInt32(Console.ReadLine());
        Person person = new Person(name, age);
        person.PrintInfo();
    }

    // клас Person (public = доступ без обмежень)
    public class Person
    {
        // поле (field) класу
        public string Name;
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
    public void Json()
    {
        // using (var fileStream = new FileStream("Users.json", FileMode.OpenOrCreate))
        // {
        //     JsonSerializer.Serialize(fileStream, users);
        // }
        //
        // List<User> users;
        // using (var fileStream = new FileStream("Users.json", FileMode.OpenOrCreate))
        // {
        //     users = JsonSerializer.Deserialize<List<User>>(fileStream);
        // }

    }
}