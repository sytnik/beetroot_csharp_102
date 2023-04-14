using Lesson20Lib;
using Newtonsoft.Json;

namespace Lesson19HomeTask;

internal static class Program
{
    private static void Main()
    {
        var classFromLib = new Class1() {Id = 1};
        Type classType = typeof(Class1);
        var props = classType.GetProperties();
        var methods = classType.GetMethods();
        var fields = classType.GetFields();
        var simplePersons = new PersonCollectionMock(100);
        var list = simplePersons.Persons;

    }
}