// call the function
// DataTypes();
// Increments();
// CheckIteration();
// Task3();
// Conditions();
// Switches();
// Ternary();
// ForForeach();
// DoWhile();
BreakContinue();
// DaysUntilSinceNY();

void CheckIteration()
{
    int num = 0;
    bool success = false;

    // !success = true -> false
    while (!success)
    {
        Console.Write("Please enter an integer: ");

        if (int.TryParse(Console.ReadLine(), out num))
        {
            // tryparse is true
            success = true;
        }
        else
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    Console.WriteLine("You entered: " + num);
}


// there is a function
void DataTypes()
{
    char ch1 = 'A'; // 1 byte
    var ch2 = 'v';
    string str = "str"; // 2 bytes * length str = 6 bytes
    var str2 = "str";
    short sh = 5; // 2 bytes  // 1 byte = 8 bit
    sh = short.MinValue;
    sh = short.MaxValue;
    int int1 = 5; // 4 bytes
    var int2 = 5;
    int1 = int.MinValue;
    int1 = int.MaxValue;
    long long1 = 5; // 8 bytes
    long1 = long.MinValue;
    long1 = long.MaxValue;
    bool bool1 = true;
    bool1 = false;
    float fl1 = float.MaxValue; // 4 bytes
    double double1 = double.MaxValue; // 8 bytes
    decimal dec1 = 5; // 16 bytes
}

void Increments()
{
    int i = 1;
    int j = 1;
    int i1 = i++;
    int i2 = ++j;
    int i3 = ++i;

    for (int k = 0; k < 5; k++)
    {
        var t = k;
    }

    for (int k = 0; k < 5; ++k)
    {
        var t = k;
    }
}

// hw3
void Task3()
{
    Console.WriteLine("Input x:");
    var strX = Console.ReadLine();
    int.TryParse(strX, out var x);
    Console.WriteLine("Input y:");
    var strY = Console.ReadLine();
    int.TryParse(strY, out var y);
    var sum = 0;
    // if (x == y)
    // {
    //     sum = x;
    // }
    // else
    // {
    //     for (var i = x; i <= y; i++)
    //     {
    //         sum += i;
    //     }
    // }
    // на один стейтмент дужки можливо не писати
    if (x == y) sum = x;
    else
        for (var i = x; i <= y; i++)
            sum += i;
    Console.WriteLine(sum);
}

// if-else
void Conditions()
{
    Console.WriteLine("input 1 or 2 or other:");
    var num = Convert.ToInt32(Console.ReadLine());
    if (num == 1)
    {
        Console.WriteLine("first option");
    }
    else if (num == 2)
    {
        Console.WriteLine("2nd option");
    }
    else
    {
        Console.WriteLine("other option");
    }
}

void Switches()
{
    Console.WriteLine("input an integer:");
    var conditionInt = Convert.ToInt32(Console.ReadLine());
    var stringRes = "";
    // switch statement
    switch (conditionInt)
    {
        case < 10:
            stringRes = "beforeTen";
            break;
        case 10:
            stringRes = "isTen";
            break;
        case > 10 and < 1000:
            stringRes = "afterTen";
            break;
        default:
            stringRes = "otherValue";
            break;
    }

    Console.WriteLine(stringRes);
    // switch expression
    stringRes = conditionInt switch
    {
        < 10 => "beforeTen2",
        10 => "isTen2",
        > 10 and < 1000 => "afterTen2",
        _ => "otherValue"
    };
    Console.WriteLine(stringRes);
}

void Ternary()
{
    Console.WriteLine("input an integer:");
    var int32 = Convert.ToInt32(Console.ReadLine());
    // if int < 100 -> result = int * 10
    // int mulIfLessThan100;
    // if (int32 < 100)
    // {
    //     mulIfLessThan100 = int32 * 10;
    // }
    // else
    // {
    //     mulIfLessThan100 = int32;
    // }
    int mulIfLessThan100 =
        int32 < 100
            ? // if(int32 < 100)
            int32 * 10
            : // if true
            int32; // if false
    Console.WriteLine(mulIfLessThan100);
    // check if int is positive number
    var formatStr = int32 <= 0
        ? // if(int32 <= 0)
        "Non-positive value"
        : // if int32 <= 0 (true)
        "Positive value"; // if false
    Console.WriteLine(formatStr);
}

void ForForeach()
{
    Console.WriteLine("input any string:");
    var str = Console.ReadLine();
    // for (ініціалізація; умова; інкремент)
    for (var i = 0; i < str.Length; i++)
    {
        Console.Write(str[i]);
    }

    // forr (r - reverse)
    for (var i = str.Length - 1; i >= 0; i--)
    {
        Console.Write(str[i]);
    }

    // foreach (type variable in collection)
    foreach (char ch in str)
    {
        Console.Write(ch);
    }
}

void DoWhile()
{
    // while
    var i = 0;
    while (i < 5) // while умова true
    {
        Console.WriteLine(i); // тіло циклу
        i++;
    }

    // do while
    i = 0;
    do
    {
        Console.WriteLine(i); // перша ітерація виконається мінімум раз
        i++;
    } while (i < 5);

    i = 6;
    do
    {
        Console.WriteLine(i); // перша ітерація виконається мінімум раз
        i++;
    } while (i < 5);
}

void BreakContinue()
{
    for (int i = 0; i < 5; i++)
    {
        Console.Write($"Iteration {i}: "); // виводиться завжди
        if (i < 3)
        {
            Console.WriteLine("skip");
            continue; // при 0, 1, 2 робимо пропуск наступного рядку
        }

        Console.WriteLine("done"); // при 3, 4 виводимо done
    }

    var str = "qwerty";
    char fourth = str[4];
    int fourth1 = str[4];
    foreach (char ch in str)
    {
        if (ch == 'r')
        {
            // в кейсі літери ‘r’ виходимо з циклу
            Console.WriteLine($"r was found {ch}");
            break;
        }

        Console.WriteLine($"searching r... {ch}"); // ‘q’,’w’,’e’
    }
}

void DaysUntilSinceNY()
{
    DateTime today = DateTime.Today;
    DateTime now = DateTime.Now;
    string nowStr = now.ToString("MM/dd/yyyy hh:mm");
    DateTime newYear = new DateTime(today.Year + 1, 1, 1);
    TimeSpan timeUntilNewYear = newYear - today;
    TimeSpan timeSinceNewYear = today - new DateTime(today.Year, 1, 1);
    Console.WriteLine(timeUntilNewYear.Days + " days left to New Year");
    Console.WriteLine(timeSinceNewYear.Days + " days passed from New Year");
}