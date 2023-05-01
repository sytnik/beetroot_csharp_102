using Lesson26;

// add person
DisplayPersonsCount();
var persons = GetAllPersons();
var matches = GetPersonsByCriteria();
var personDtos = GetPersonDtos();
AddPerson();
Console.WriteLine();

void AddPerson()
{
    var person = new Person
    {
        Id = 51,
        FirstName = "John",
        LastName = "Smith",
        Age = 51,
        Address = "some address",
        Gender = "male"
    };
    using var context = new SampleContext();
    context.Persons.Add(person);
    context.SaveChanges();
}


// display all persons
PersonDto[] GetPersonDtos()
{
    using var context = new SampleContext();
    return context.Persons
        .Select(person =>
            new PersonDto(person.Id,
                $"{person.FirstName} {person.LastName}"))
        .ToArray();
}

// get persons by criteria
Person[] GetPersonsByCriteria()
{
    using var context = new SampleContext();
    return context.Persons
        .Where(person => person.Id > 20)
        .OrderBy(person => person.FirstName)
        .ThenBy(person => person.LastName)
        .ToArray();
}

// get all persons
List<Person> GetAllPersons()
{
    using var context = new SampleContext();
    return context.Persons.ToList();
}

// display persons count
void DisplayPersonsCount()
{
    using var context = new SampleContext();
    var count = context.Persons.Count();
    Console.WriteLine($"Count: {count}");
}

// add person dto
public record PersonDto(int Id, string FullName);