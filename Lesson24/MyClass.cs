namespace Lesson24;

public interface IMyInterface
{
    int MyMethod(int a, int b);
}

public class MyClass
{
    private readonly IMyInterface _myInterface;

    public MyClass()
    {
    }

    public MyClass(IMyInterface myInterface)
    {
        _myInterface = myInterface;
    }

    public int MyMethod(int a, int b)
    {
        return _myInterface.MyMethod(a, b);
    }

    public int MulMethod(params int[] args)
    {
        var res = 1;
        foreach (var val in args)
        {
            res *= val;
        }

        return res;
    }
}