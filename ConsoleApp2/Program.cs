namespace Lesson4;

// default template with overload
class Program
{
    // (5,7) -> 18
    // 5 + sum(5+1,7) // 5 + 13
    // 6 + sum(6+1,7) 6 + 7
    // 7                
    static int Sum(int from, int to)
    {
        if (from > to) return Sum(to, from);
        if (from == to) return from;
        return from + Sum(from + 1, to);
    }

    static string ConcatOutput(string title, params string[] words)
    {
        string head = title + ": ";
        foreach (var word in words) head += word + ", ";
        return head;
        // some comment
    }

    static void OutputParams(params object[] words)
    {
        foreach (var w in words)
        {
            Console.WriteLine(w);
        }
    }

    static void SumParameters(out int sum, params int[] parameters)
    {
        sum = 0;
        foreach (var par in parameters)
        {
            sum += par;
        }
    }

    static void SumParameters(ref long sum, params long[] parameters)
    {
        // sum = 0;
        // foreach (var par in parameters)
        // {
        //     sum += par;
        // }
    }

    static void Main()
    {
        int sum = 0;
        SumParameters(out sum, 1, 2);
        Console.WriteLine(sum);
        // var sum1 = 0;
        SumParameters(out var sum1, 3, 4);
        Console.WriteLine(sum1);
        SumParameters(out sum, 1, 2, 3, 4, 5);
        bool isParsed = int.TryParse("123", out sum);
        if (isParsed)
        {
            Console.WriteLine(sum);
        }

        bool isOdd = TrySumIfOdd(5, 10, out sum);
        OutputParams("123", "345", 553);
        string concat = ConcatOutput("title");
        string concat2 = ConcatOutput("title", "w1", "w2");
        int sum5 = Sum(5, 7);
        Console.WriteLine(sum5);
        int value = 0;
        IncrementAndOutput(ref value); // лише required
        IncrementAndOutput(ref value, 10);
        IncrementAndOutput(ref value, 10, "123");
        IncrementAndOutput(value);
        Console.WriteLine();
        var x = 1;
        Console.WriteLine(OverLd(x) + OverLd(x, x));
        var output = ConcatOutput("mytitle", "w1", "w2", "w3");
        int sum6 = 0;
        Console.WriteLine("Sum is " + sum6 + ", isOdd=" + isOdd);
    }

    static void IncrementAndOutput(int value)
    {
        Console.WriteLine("Value passed " + ++value);
    }

    static void IncrementAndOutput(ref int value, int additionalVal = 0,
        string additionalVal2 = "")
    {
        Console.WriteLine("Value passed " + ++value + additionalVal);
    }

    static int OverLd(int x) => x;

    static int OverLd(int x, int y) => x + y;

    static bool TrySumIfOdd(int start, int end, out int sum)
    {
        sum = 0;
        if (start == end) return false;

        if (start > end)
        {
            int temp = start;
            start = end;
            end = temp;
        }

        for (var i = start + 1; i < end; i++)
            sum += i;
        // чи є непарним?
        bool isOdd = sum % 2 == 1;
        return isOdd;
    }
}