namespace Lesson11Lib;

public class TestClass
{
    private UtilityClass Utility { get; set; }

    public TestClass()
    {
        Utility = new UtilityClass();
        Utility.Id = 1;
        Utility.Age = 20;
        Utility.Name = "name";
    }
}