using Lesson24;

namespace Lesson24Tests;

public class SerializationTests
{
    [Theory, InlineData(200)]
    public void TestXmlDeserialization(int count)
    {
        var persons = XmlSerialization.DeserializePersons().ToList();
        Assert.NotEmpty(persons);
        Assert.Equal(count, persons.Count);
    }
}