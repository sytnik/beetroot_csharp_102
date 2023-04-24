using Bogus;
using Lesson24;

namespace Lesson24Tests;

public class PersonClassTests
{
    [Fact]
    public void GetFullName_ReturnsExpectedResult()
    {
        // Arrange
        var faker = new Faker();
        var firstName = faker.Name.FirstName();
        var lastName = faker.Name.LastName();
        var person = new PersonClass { FirstName = firstName, LastName = lastName };
        var expectedFullName = $"{firstName} {lastName}";
        // Act
        var result = person.GetFullName();
        // Assert
        Assert.Equal(expectedFullName, result);
    }

}