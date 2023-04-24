namespace Lesson24;

public interface IMyDependency
{
    int GetNumber();
    string GetString();
}

internal class MyClassForMoq
{
    private readonly IMyDependency _myDependency;

    public MyClassForMoq(IMyDependency myDependency)
    {
        _myDependency = myDependency;
    }

    public string MyMethod()
    {
        int number = _myDependency.GetNumber();
        string str = _myDependency.GetString();
        return $"Number: {number}, String: {str}";
    }

    public int MyOtherMethod()
    {
        return _myDependency.GetNumber();
    }
}
