using Lesson11Lib;

namespace Lesson8;

public class Program
{
    public int SomeValue;
    public static void Main()
    {
        UtilityClass util = new UtilityClass();
        util.Id = 1;
        // util.Name = "name";
        PersonRec rec = new();
        rec = new("FirstName", "LastName", 20);
        rec = rec with {Age = 21};
        GC.Collect();
        var date = DateTime.Now;
        // create new string with dd:mm
        string str = date.ToString("dd:mm");

        List<int> ints = new List<int> {1, 2, 3};
        ints.Add(4);
        int[] ints2 = new[] {1, 2, 3};
        Array.Resize(ref ints2, ints2.Length + 1);
        ints2[ints2.Length - 1] = 4;
       
    }

    public record PersonRec(string FirstName = "someName",
        string LastName = "someSurname", int Age = 18);


    // клас Person (public = доступ без обмежень)
    public class Person
    {
        // поле (field) класу
        public readonly int Id;

        // властивості (property) класу
        public string Name { get; set; }
        public int Age { get; set; }

        // конструктор (constructor) класу
        public Person()
        {
        }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Person(string Name, int age)
        {
            this.Name = Name;
            Age = age;
        }

        public Person(Person otherPerson)
        {
            Name = otherPerson.Name;
            Age = otherPerson.Age;
        }

        // метод (method) класу
        public void PrintInfo() =>
            Console.WriteLine($"Name: {Name}, Age: {Age}");

        public static void PrintInfoStatic(string name, int age) =>
            Console.WriteLine($"Name: {name}, Age: {age}");
    }
}