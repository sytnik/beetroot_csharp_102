namespace Lesson28.Models;

// entity class person table
public sealed record Person : EntityWithId
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string? Address { get; set; }
    public List<Order> Orders { get; set; }

    public Person()
    {
    }

    public Person(int id, string firstName, string lastName, int age,
        string gender, string? address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Gender = gender;
        Address = address;
    }
}