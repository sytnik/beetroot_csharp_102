namespace Lesson4;

// default template with overload
class Program
{
    static void Main()
    {
        var x = 1;
        Console.WriteLine(OverLd(x) + OverLd(x, x));
    }

    static int OverLd(int x) => x;
    static int OverLd(int x, int y) => x + y;
    // making changes
}