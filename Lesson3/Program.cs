Task3();
// Conditions();
// Switches();
// Ternary();
// ForForeach();
// DoWhile();
// BreakContinue();

// hw3
void Task3()
{
    Console.WriteLine("Input x:");
    var line1 = Console.ReadLine();
    int.TryParse(line1, out var x);
    Console.WriteLine("Input y:");
    var line2 = Console.ReadLine();
    int.TryParse(line2, out var y);
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
        case > 10:
            stringRes = "afterTen";
            break;
    }

    Console.WriteLine(stringRes);
    // switch expression
    stringRes = conditionInt switch
    {
        < 10 => "beforeTen2",
        10 => "isTen2",
        > 10 => "afterTen2"
    };
    Console.WriteLine(stringRes);
}

void Ternary()
{
    Console.WriteLine("input an integer:");
    var int32 = Convert.ToInt32(Console.ReadLine());
    // if int < 100 -> result = int * 10
    var mulIfLessThan100 = int32 < 100 ? int32 * 10 : int32;
    Console.WriteLine(mulIfLessThan100);
    // check if int is positive number
    var formatStr = int32 <= 0 ? "Non-positive value" : "Positive value";
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
    foreach (var ch in str)
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
    foreach (int ch in str)
    {
        if (ch == 'r')
        {
            // в кейсі літери ‘r’ виходимо з циклу
            break;
        }

        Console.Write($"{ch}"); // ‘q’,’w’,’e’
    }
}