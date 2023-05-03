using Lesson26;
using Microsoft.EntityFrameworkCore;

// add person
DisplayPersonsCount();
var persons = GetAllPersons();
var matches = GetPersonsByCriteria();
var personDtos = GetPersonDtos();
// AddPerson();
// DeletePerson(52);
// DeletePerson(51);
// UpdatePerson2(1);
// var orders = GetAllOrders();
// var order = GetOrderWithPersonManually(1);
GetRelatedData();
Console.WriteLine();

void GetRelatedData()
{
    using var context = new SampleContext();
    var orders = context.Orders
        .Include(o => o.Person)
        .ToArray();
    var persons = context.Persons
        .Include(p => p.Order)
        .ToArray();
    var personsWithOrders = context.Persons
        .Include(p => p.Order)
        .Where(p => p.Order != null)
        .ToArray();
    Console.WriteLine();
}

Order GetOrderWithPersonManually(int id)
{
    using var context = new SampleContext();
    var order = context.Orders.FirstOrDefault(order => order.Id == id);
    order.Person = context.Persons.FirstOrDefault(person => person.Id == order.PersonId);
    return order;
}

// get all orders
Order[] GetAllOrders()
{
    using var context = new SampleContext();
    return context.Orders.ToArray();
}

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

// update person (1st case)
void UpdatePerson1(int id)
{
    using var context = new SampleContext();
    var person = context.Persons.FirstOrDefault(person => person.Id == id);
    if (person is null) return;
    person.FirstName = "Bob";
    context.SaveChanges();
}

// update person (2nd case) - batch update
void UpdatePerson2(int id)
{
    // new data
    var newPerson = new Person
    {
        Id = id,
        FirstName = "Updated",
        LastName = "User",
        Age = 25,
        Address = "Updated address",
        Gender = "male"
    };
    using (var context = new SampleContext())
    {
        // get entity from database
        var dbPerson = context.Persons.FirstOrDefault(person => person.Id == id);
        if (dbPerson is null) return;
        // update entity
        context.Entry(dbPerson).CurrentValues.SetValues(newPerson);
        context.SaveChanges();
    }

    Console.WriteLine("Updated");
}

void DeletePerson(int id)
{
    using var context = new SampleContext();
    // entity or null
    var personToDelete = context.Persons.FirstOrDefault(person => person.Id == id);
    // do not delete if null
    if (personToDelete is null) return;
    context.Persons.Remove(personToDelete);
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