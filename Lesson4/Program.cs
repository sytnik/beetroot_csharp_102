var x = 1;
Console.WriteLine(OverLd(x)); // + OverLd(x, x));

// will not compile
static int OverLd(int x) => x;
// static int OverLd(int x, int y) => x + y;