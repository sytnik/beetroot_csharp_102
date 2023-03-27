using Lesson11Lib;

namespace Lesson8;

public class Program
{
    public int SomeValue;

    public static void Main()
    {
        // List<int> list = new List<int>() {1, 5, 3, 2, 4};
        // Sorter sorter = new Sorter();
        // sorter.SetSortStrategy(new BubbleSort());
        // sorter.Sort(list);
        // list.Reverse();
        // sorter.SetSortStrategy(new QuickSort());
        // sorter.Sort(list);
        Employee person = new Employee(1, "somePerson", "department");
        string dep = person.DepartmentName;
        person.DepartmentName = "dep";
        person.DepartmentName = "department";
        Console.WriteLine(person);
        Console.ReadLine();
    }
}

public class Person : IEquatable<Person>
{
    protected int Id { get; set; }
    protected string Name { get; set; }
    
    public Person()
    {
    }

    public Person(int id) => Id = id;

    public Person(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() =>
        $"Person: Id:{Id}, Name:{Name}";

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Person) obj);
    }

    public bool Equals(Person? other)
    {
        return other != null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}

public sealed class Employee : Person
{
    private string _departmentName;
    
    public string DepartmentName
    {
        get => $"name: {_departmentName}";
        set => _departmentName = value.Length > 5 ? value : _departmentName;
    }
    public string DepartmentName1 { get; set; }
    public void SetDepartmentName(string dep) =>
        _departmentName = dep.Length > 5 ? dep : _departmentName;
    public Employee(int id, string departmentName) : base(id) =>
        _departmentName = departmentName;
    // public Employee(int id, string departmentName)
    // {
    //     Id = id;
    //     DepartmentName = departmentName;
    // }

    public Employee(int id, string name, string departmentName) : base(id, name) =>
        _departmentName = departmentName;

    public override string ToString() =>
        $"{base.ToString()}, departmentName:{_departmentName}";
}

public abstract class ISortStrategy
{
    public abstract void Sort(List<int> list);
}

public class BubbleSort : ISortStrategy
{
    public override void Sort(List<int> list) => Console.WriteLine("Sorting using bubble sort");
}

public class QuickSort : ISortStrategy
{
    public override void Sort(List<int> list) => Console.WriteLine("Sorting using quick sort");
}

public class Sorter
{
    private ISortStrategy _sortStrategy;
    public void SetSortStrategy(ISortStrategy sortStrategy) => _sortStrategy = sortStrategy;
    public void Sort(List<int> list) => _sortStrategy.Sort(list);
}