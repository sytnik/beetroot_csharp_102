namespace Lesson26;

// entity class
// person table
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string? Address { get; set; }
    public Order Order { get; set; }

    public Person()
    {
    }
}