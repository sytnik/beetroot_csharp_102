namespace Lesson11Lib;

public class UtilityClass
{
    public int Id { get; set; }

    protected internal string Name { get; set; }

    // from this class only!
    private int _age;

    //property
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0)
            {
                _age = value;
            }
        }
    }

    // method
    public void SetAge(int newAge) =>
        _age = newAge >= 0 ? newAge : _age;

    // ctor
    public UtilityClass(){}
    public UtilityClass(int age)
    {
        _age = age >= 0 ? age : _age;
    }
}