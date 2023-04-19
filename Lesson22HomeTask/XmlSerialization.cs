using System.Text;
using System.Xml.Serialization;

namespace Lesson22HomeTask;

public static class XmlSerialization
{
    public static void SerializePersons(this IEnumerable<Person> persons)
    {
        Type personType = typeof(List<Person>);
        XmlSerializer serializer = new XmlSerializer(personType);
        StringWriter writer = new StringWriter();
        serializer.Serialize(writer, persons);
        string serializedText = writer.ToString();
        // default Encoding.UTF8
        File.WriteAllText("persons.xml", serializedText, Encoding.Unicode);
    }

    public static IEnumerable<Person> DeserializePersons()
    {
        string personsData = File.ReadAllText("persons.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
        StringReader reader = new StringReader(personsData);
        return (List<Person>) serializer.Deserialize(reader);
    }
}