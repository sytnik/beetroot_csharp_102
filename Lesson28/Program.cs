using Lesson28;
using Lesson28.Models;
using Microsoft.EntityFrameworkCore;

// add person
DisplayPersonsCount();
var persons = GetAllPersons();
var matches = GetPersonsByCriteria();
var personDtos = GetPersonDtos();
// CreateOrderWithDetails();
// AddPerson(new Person(52, "John", "Smith", 52, "male", "some address"));
// DeletePerson(52);
// DeletePerson(51);
// UpdatePerson2(1);
// var orders = GetAllOrders();
// var order = GetOrderWithPersonManually(1);
GetManyToMany();
GetRelatedData();
Console.WriteLine();

void GetManyToMany()
{
    using var context = new SampleContext();
    var orders = context.Orders
        .Include(order => order.Products)
        .ToArray();
    var products = context.Products
        .Include(product => product.Orders)
        .ToArray();

    var personProducts = context.Persons
        // include orders
        .Include(person => person.Orders)
        // then include products
        .ThenInclude(order => order.Products)
        // first person with orders    
        .Where(person => person.Orders
            .Any(order => order.Products.Any()))
        // get products from orders
        .SelectMany(person => person.Orders)
        .SelectMany(order => order.Products)
        .Distinct()
        .OrderBy(product => product.Id).ToArray();
    Console.WriteLine();
}


void CreateOrderWithDetails()
{
    using var context = new SampleContext();
    // create order with details
    var newOrder = new Order(1, 1, "Some info");
    context.Orders.Add(newOrder);
    var orderDetails = new OrderDetails(1, 1, "Some address");
    context.OrderDetails.Add(orderDetails);
    context.SaveChanges();
    // get order with details
    var orderWithDetails = context.Orders
        .Include(order => order.OrderDetails)
        .FirstOrDefault(order => order.Id == 1);
    Console.WriteLine();
}

void GetRelatedData()
{
    using var context = new SampleContext();
    var orders = context.Orders
        .Include(o => o.Person)
        .ToArray();
    var persons = context.Persons
        .Include(p => p.Orders)
        .ToArray();
    var personsWithOrders = context.Persons
        .Include(p => p.Orders)
        .Where(p => p.Orders.Count > 1)
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

void AddPerson(Person person)
{
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
namespace Lesson28
{
    public record PersonDto(int Id, string FullName);
}