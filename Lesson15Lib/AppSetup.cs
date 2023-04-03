namespace Lesson15Lib;

public static class AppSetup
{
    // compile-time constant
    public const string FilePath = "MyPhoneBook.json";

    public static void SwapGeneric<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
    
    public static int Factorial(int n)
    {
        if (n == 0)
        {
            return 1;
        }
        else
        {
            return n * Factorial(n - 1);
        }
    }
    
    // public static T FactorialGen<T>(T n)
    // {
    //     if (n == 0)
    //     {
    //         return 1;
    //     }
    //     else
    //     {
    //         return n * FactorialGen(n - 1);
    //     }
    // }
    public static T Factorial<T>(T n) where T : struct, IComparable, IConvertible
    {
        int intValue = Convert.ToInt32(n);

        if (intValue == 0)
        {
            return (T)Convert.ChangeType(1, typeof(T));
        }
        else
        {
            return (T)Convert.ChangeType(intValue * Factorial(intValue - 1), typeof(T));
        }
    }
}