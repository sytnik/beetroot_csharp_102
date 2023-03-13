namespace Lesson5;

class Program
{
    static void Main()
    {
        int[] source = { 5, 8, 6, 7, 3, 35, 4 };
        int[] sorted1 = Sort(SortAlgorithmType.Selection, OrderBy.Asc, source);
        // SampleArray();
        MultidimensionalArray();
        JaggedArray();
        TupleTest();
        // LINQ
        // [] - array of some type
        char[] chArr = new[] { 'a', 's', 'd' };
        
        string str = string.Join(',', source);
        string str1 = string.Join(",*, ", source);
        string str2 = string.Join("", chArr);
        
        Console.WriteLine(string.Join(", ", sorted1));
        int[] sorted2 = Sort(SortAlgorithmType.Bubble, OrderBy.Desc, source);
        Console.WriteLine(string.Join(", ", sorted2));
        int[] sorted3 = Sort(SortAlgorithmType.Insertion, OrderBy.Asc, 5, 8, 6, 7, 3, 35, 4);
        int[] sorted4 = Sort(SortAlgorithmType.Insertion, OrderBy.Asc, new[]{5, 8, 6, 7, 3, 35, 4});
        Console.WriteLine(string.Join(", ", sorted3));
        var t = TwoBeforeLastByRange(1, 2, 3, 4, 5);
        var a = Sort3d();
    }

    static int[,,] Sort3d()
    {
        // sample 3x4x5 array
        int[,,] myArray = new int[3, 4, 5]
        {
            { { 6, 5, 4, 3, 2 }, { 1, 2, 3, 4, 5 }, { 6, 5, 4, 3, 2 }, { 1, 2, 3, 4, 5 } },
            { { 9, 8, 7, 6, 5 }, { 4, 5, 6, 7, 8 }, { 9, 8, 7, 6, 5 }, { 4, 5, 6, 7, 8 } },
            { { 3, 2, 1, 0, 9 }, { 8, 7, 6, 5, 4 }, { 3, 2, 1, 0, 9 }, { 8, 7, 6, 5, 4 } }
        };
        // create an one-dimensional temp array
        int[] flatArray = new int[myArray.Length];
        // transfer the 3d array to 1d
        Buffer.BlockCopy(myArray, 0, flatArray,
            0, sizeof(int) * myArray.Length);
        // sort 1d array
        Array.Sort(flatArray);
        // transfer the sorted 1d array back to the 3d one
        Buffer.BlockCopy(flatArray, 0, myArray,
            0, sizeof(int) * myArray.Length);
        return myArray;
    }


    static int[] TwoBeforeLastByRange(params int[] array)
    {
        // from array[3] from last to array[1] from last
        return array[^3..^1];
    }

    // gereral Sort method
    static int[] Sort(SortAlgorithmType type, OrderBy order, params int[] source)
    {
        switch (type)
        {
            case SortAlgorithmType.Selection:
                return SelectionSort(order, source);
            case SortAlgorithmType.Bubble:
                return BubbleSort(order, source);
            case SortAlgorithmType.Insertion:
                return InsertionSort(order, source);
            default:
                return source;
        }
    }

    // SelectionSort
    static int[] SelectionSort(OrderBy order, params int[] source)
    {
        int n = source.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (order == OrderBy.Asc && source[j] < source[minIndex])
                {
                    minIndex = j;
                }
                else if (order == OrderBy.Desc && source[j] > source[minIndex])
                {
                    minIndex = j;
                }
            }

            int temp = source[minIndex];
            source[minIndex] = source[i];
            source[i] = temp;
        }

        return source;
    }

    // BubbleSort
    static int[] BubbleSort(OrderBy order, params int[] source)
    {
        int n = source.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (order == OrderBy.Asc && source[j] > source[j + 1])
                {
                    int temp = source[j];
                    source[j] = source[j + 1];
                    source[j + 1] = temp;
                }
                else if (order == OrderBy.Desc && source[j] < source[j + 1])
                {
                    int temp = source[j];
                    source[j] = source[j + 1];
                    source[j + 1] = temp;
                }
            }
        }

        return source;
    }

    // InsertionSort
    static int[] InsertionSort(OrderBy order, params int[] source)
    {
        int n = source.Length;
        for (int i = 1; i < n; i++)
        {
            int key = source[i];
            int j = i - 1;

            if (order == OrderBy.Asc)
            {
                while (j >= 0 && source[j] > key)
                {
                    source[j + 1] = source[j];
                    j--;
                }
            }
            else if (order == OrderBy.Desc)
            {
                while (j >= 0 && source[j] < key)
                {
                    source[j + 1] = source[j];
                    j--;
                }
            }

            source[j + 1] = key;
        }

        return source;
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

    static void SampleArray()
    {
        //init array
        int[] arr = new int[0];
        arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        // index
        int element = arr[5];
        // Index
        Index index = new Index(1);
        int second = arr[index];
        //Range
        var twoElements = arr[0..2];
        var twoLast = arr[^1..^0];
        var two = arr[^2..];
        var two1 = arr[^2..];
        var two2 = arr[^3..2];
        // first elem as new array
        Range range1 = new Range(0, 1);
        var firstButArr = arr[range1];
        // take four first
        Range range2 = new Range(new Index(0), 5);
        var firstFour = arr[range2];
    }

    static void MultidimensionalArray()
    {
        // 2-dimensional - table
        // 0.0 0.1
        // 1.0 1.1
        // 2.0 2.1
        // 0 0
        int[,] arr = new int[4, 2];
        //3-dimensional - cube
        int[,,] array1 = new int[4, 2, 3];
        // Two-dimensional array.
        int[,] array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
        // The same array with dimensions specified.
        int[,] array2Da = new int[4, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
    }

    static void JaggedArray()
    {
        // 0 0 0 0 0
        // 0 0 0 0
        // 0 0     
        // jagged array with 3 different rows
        int[][] jaggedArray = new int[3][];
        jaggedArray[0] = new int[5];
        jaggedArray[1] = new int[4];
        jaggedArray[2] = new int[2];
        // init array
        int[][] jaggedArray2 = new int[][]
        {
            new int[] { 1, 3, 5, 7, 9 },
            new int[] { 0, 2, 4, 6 },
            new int[] { 11, 22 } // 2
        };
        int[] secondRow = jaggedArray2[1];
        int val = jaggedArray2[2][1];

        int oneElement = 100;
        int[] oneElementArray = new[] { 100 };
        int value = oneElementArray[0]; // 100
    }

    static void TupleTest()
    {
        // tuple (not the valuetuple !) - кортеж
        // with type
        Tuple<int, string, string> person = new Tuple<int, string, string>(1, "Steve", "Jobs");

        // create from arguments
        var person1 = Tuple.Create(1, "Steve", "Jobs");
        int x = person1.Item1; // returns 1
        string y = person1.Item2; // returns "Steve"
        string z = person1.Item3; // returns "Jobs"
        // person1.Item1 = 3;

        // valuetuple - кортеж значень (.net fw 4.7+ or System.ValueTuple)
        // long notation
        ValueTuple<int, string, string> person4 = (1, "Bill", "Gates");
        int q = person4.Item1; // returns 1
        string w = person4.Item2; // returns "Bill"
        string e = person4.Item3; // returns "Gates"

        // new notation
        var person2 = (1, "Bill", "Gates");
        //equivalent Tuple
        var person3 = Tuple.Create(1, "Bill", "Gates");

        // big valuetuple
        var numbers = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);

        // named tuple
        (int Id, string FirstName, string LastName) person5 = (1, "Bill", "Gates");
        int a = person5.Id; // returns 1
        string s = person5.FirstName; // returns "Bill"
        string d = person5.LastName; // returns "Gates"
        person5.FirstName = "other";

        // var tuple
        // var - compile type!
        // simple init
        var sometuple = (1, "someText", Guid.NewGuid(), 1.34);
        // var text = sometuple[1]; // no index
        var text = sometuple.Item2;
        // named tuple initialization
        // GUID (globally unique identifier)
        double someDouble = 1.34;
        (int id, string textField, Guid someGuid, double doubleVal) tuple2 =
            (1, "someText", Guid.NewGuid(), someDouble);
        var val1 = tuple2.someGuid;

        // named tuple (userId, firstName, isActive)
        // [] of tuples
        (int userId, string firstName, bool isActive)[] users =
            new (int userId, string firstName, bool isActive)[]
            {
                (1, "John", true),
                (2, "Jane", true),
                (3, "Hanz", false)
            };
        string secondName = users[1].firstName; //Jane
        bool thirdUserIsActive = users[2].isActive; //false
    }
}