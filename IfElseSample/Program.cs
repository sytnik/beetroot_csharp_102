// початок виконання
// викликаємо метод SumBetween

int sum = SumBetween(5, 10);

// виводимо результат методу
Console.WriteLine(sum);

// визначаємо сам метод розрахунку
static int SumBetween(int x, int y)
{
    // змінна суми
    int sum = 0;
    // умова рівності параметрів
    if (x == y)
    {
        sum = x;
    }
    // якщо х більше у
    else if (x < y)
    {
        for (int i = x; i <= y; i++)
        {
            sum += i;
        }
    }
    // якщо параметри переплутані - рахуємо від у до х
    else
    {
        for (int i = y; i <= x; i++)
        {
            sum += i;
        }
    }

    // повертаємо результат
    return sum;
}