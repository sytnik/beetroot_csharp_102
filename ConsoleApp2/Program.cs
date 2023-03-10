using System.Linq;

namespace Lesson4;

// default template with overload
class Program
{
    private int SomeImportantNumber1 = 16;

    enum MyEnum1
    {
        Elem1,
        Elem2,
        Elem3 = 10,
        Elem4 = 10,
        SomeImportantNumber = 15,
        Elem6
    }

    enum MyEnum : long // somename - int value from 0
    {
        Elem1, //0
        Elem2, //1
        Elem3 = 10,
        Elem4 = 10,
        SomeImportantNumber = 15, //15
        Elem6 // 15 + 1
    }

    enum SortAlgorithmType
    {
        Selection,
        Bubble,
        Insertion
    }

    enum OrderBy
    {
        Asc,
        Desc
    }

    // just extra
    int[] Sort(SortAlgorithmType type, OrderBy order,
        params int[] source) => source;

    static void Main()
    {
        Console.WriteLine(MyEnum.Elem3); // element name, "Elem3"
        Console.WriteLine((int)MyEnum.SomeImportantNumber); // elem value
        // [10, 11, 12]
        int sum = SumBetween(10, 12);
        var max = MaxOf(7, 6, 8, 9);
        int[] arr = CreateRandArray(10);
    }

    static int SumBetween(int x, int y)
    {
        // calculate the sum of all numbers between x and y
        int sum = 0;

        if (x == y)
        {
            sum = x;
        }
        else if (x < y)
        {
            for (int i = x; i <= y; i++) // from x to y
            {
                sum += i;
            }
        }
        else
        {
            for (int i = y; i <= x; i++) // from y to x
            {
                sum += i;
            }
        }

        return sum;
    }

    static int[] CreateRandArray(int size)
    {
        // init array
        int[] ints = new int[size];
        // init random
        Random random = new Random();
        for (int i = 0; i < size; i++)
        {
            ints[i] = random.Next(-size, size);
        }

        return ints;
    }


    int[] SelectionSort(params int[] source) => source;

    // 2 params, not a collection!
    int MaxOf(int first, int second) => Math.Max(first, second);

    // params overload, array of elements
    static int MaxOf(params int[] ints)
    {
        // ints {7, 6, 8, 9} * max index = length - 1
        // ints[0] = 7
        // ints[3] = 9
        string s1 = "qwerty";
        int length1 = s1.Length;
        int length = ints.Length;
        int sum = ints.Sum();
        int min = ints.Min();
        int max = ints.Max();
        int maxElem = Math.Max(ints[0], ints[1]);
        // for loop
        for (int i = 0; i < ints.Length; i++)
        {
            if (i > 2)
                Console.WriteLine(ints[i]);
        }

        // not modifying ints
        foreach (int someVar in ints)
        {
            // someVar += 1;
            Console.WriteLine(someVar);
        }

        return max;
    }

    // 4 params
    int MaxOf(int first, int second, int trird, int fourth) =>
        Math.Max(Math.Max(Math.Max(first, second), trird), fourth);

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

    static void Main1()
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