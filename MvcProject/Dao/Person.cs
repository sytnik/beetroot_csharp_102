namespace MvcProject.Dao;

public sealed record Person : EntityWithId
{
    [Required(ErrorMessage = "First Name is required.")]
    [MinLength(3, ErrorMessage = "First Name must be at least 3 characters long.")]
    [StringLength(100, ErrorMessage = "First Name cannot be longer than 100 characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    public string Gender { get; set; }

    [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.")]
    public string? Address { get; set; }

    public List<Order> Orders { get; set; }

    public Person()
    {
    }

    public Person(int id, string firstName, string lastName, int age, string gender, string? address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Gender = gender;
        Address = address;
    }
}