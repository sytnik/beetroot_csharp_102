// TestVariables();
// TestInput();
// Dates();
RandomNumbers();

void RandomNumbers()
{
    var rand = new Random();
    var r1 = rand.Next();
    var r2 = rand.Next(100);
    var r3 = rand.Next(100,1000);
    var r4 = rand.NextDouble();
    Console.ReadLine();
}

void Dates()
{
    var datetime = DateTime.Now;
    var date = DateOnly.FromDateTime(datetime);
    var time = TimeOnly.FromDateTime(datetime);
    Console.WriteLine("Input date:");
    var strDate = Console.ReadLine();
    DateOnly.TryParse(strDate, out date);
    Console.ReadLine();
}


void TestInput()
{
    var input = "Input value of ";
    Console.WriteLine(input + "x");
    var stringX = Console.ReadLine();
    // just parse
    // var intX = int.Parse(stringX);
    // parse with check
    float intX = -1;
    bool hasconverted = float.TryParse(stringX, out intX);
    // check if correct
    var bool1 = hasconverted;
    var bool2 = intX != default;
    Console.WriteLine(input + "y");
    var stringY = Console.ReadLine();
    var convertY = Convert.ToInt32(stringY);
    Console.WriteLine("X is " + intX + " Y is " + stringY);
}

void TestVariables()
{
    // data types and variables
    int value1 = 5;
    double v2 = 5.8;
    char ch = '1';
    string str = "123";
    bool b = true;
    decimal c = 10;
// operators
    var sum = value1 + v2;
    var diff = value1 - v2;
    var mul = value1 * v2;
// func
    var x = 5;
    var y = 10;
// max
    var max = Math.Max(x, y);
// pi
    var pi = 2 * Math.PI * x;
// abs
    var abs = Math.Abs(x) * Math.Sin(x);
// multiple operands
    var res4 = -6 * Math.Pow(x, 3) + 5 * Math.Pow(x, 2) - 10 * x + 15;
// var 
    var someint = 5;
    var somefloat = 1.3;
    double mul2 = someint * somefloat;
    long intmul = someint * someint;
// multiply
    short val1 = short.MaxValue;
    int val2 = int.MaxValue;
    long res = Math.BigMul(val2, val2);
    long res2 = val1 * val2;
    Console.WriteLine("Mul res " + res);
    Console.WriteLine("Mul res long " + res2);
}