namespace Lesson8;

public class Program
{
    public static void Main()
    {
        Author author = new Author(new List<Book> {new Book()});
        author.Books.Add(new Book());
        Book book = new Book();
        book.Id = 1;
        book.Authors = new();
        // Lesson8.Library.Book book = new Lesson8.Library.Book();
        // no data
        Person person = new Person();
        Person person3 = new Person(1, "user3");
        // person3.Id = 15;
        // with data from ctor
        Person person1 = new Person("user1", 25);
        Person testUser = person1;
        // from other object of the same type
        // Person = type/class
        // person2 = object of type Person
        Person person2 = new Person(person1);
        person1.Name = "changedName";
        // instance method = method of the object
        person1.PrintInfo();
        // type
        Person.PrintInfoStatic(person.Name, person1.Age);
    }


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