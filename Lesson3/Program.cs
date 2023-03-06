Task3();

void Task3()
{
    Console.WriteLine("Input x:");
    int.TryParse(Console.ReadLine(), out var x);
    Console.WriteLine("Input y:");
    int.TryParse(Console.ReadLine(), out var y);
    var sum = 0;
    if (x == y) sum = x;
    else
        for (var i = x; i <= y; i++)
            sum += i;
    Console.WriteLine(sum);
}