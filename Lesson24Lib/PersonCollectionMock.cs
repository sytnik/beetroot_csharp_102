using Bogus;

namespace Lesson22Lib;

public class PersonCollectionMock
{
    public List<SimplePerson> Persons { get; set; }

    public PersonCollectionMock(int count)
    {
        var personFaker = new Faker<SimplePerson>()
            .RuleFor(person => person.Id, faker => faker.UniqueIndex)
            .RuleFor(person => person.FirstName, faker => faker.Name.FirstName())
            .RuleFor(person => person.LastName, faker => faker.Name.LastName())
            .RuleFor(person => person.Email, (faker, person) =>
                faker.Internet.Email(person.FirstName, person.LastName))
            .RuleFor(person => person.DateOfBirth,
                faker => faker.Date.Past(30, DateTime.Now.AddYears(-18)));
        Persons = personFaker.Generate(count);
    }
}