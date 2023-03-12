namespace Lesson5;

class Program
{
    static void Main()
    {
        int[] source = {5, 8, 6, 7, 3, 35, 4};
        int[] sorted1 = Sort(SortAlgorithmType.Selection, OrderBy.Asc, source);
        Console.WriteLine(string.Join(", ", sorted1));
        int[] sorted2 = Sort(SortAlgorithmType.Bubble, OrderBy.Desc, source);
        Console.WriteLine(string.Join(", ", sorted2));
        int[] sorted3 = Sort(SortAlgorithmType.Insertion, OrderBy.Asc, 5, 8, 6, 7, 3, 35, 4);
        Console.WriteLine(string.Join(", ", sorted3));
    }

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

    void SampleArray()
    {
        //init array
        int[] arr = new int[0];
        arr = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
        // index
        int element = arr[5];
        // Index
        Index index = new Index(1);
        int second = arr[index];
        //Range
        var twoElements = arr[0..2];
        var twoLast = arr[^1..^0];
        // first elem as new array
        Range range1 = new Range(0, 1);
        var firstButArr = arr[range1];
        // take four first
        Range range2 = new Range(new Index(0), 5);
        var firstFour = arr[range2];
    }

    void MultidimensionalArray()
    {
        // 2-dimensional - table
        int[,] arr = new int[4, 2];
        //3-dimensional - cube
        int[,,] array1 = new int[4, 2, 3];
        // Two-dimensional array.
        int[,] array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
        // The same array with dimensions specified.
        int[,] array2Da = new int[4, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
    }

    void JaggedArray()
    {
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
            new int[] { 11, 22 }
        };

    }
}