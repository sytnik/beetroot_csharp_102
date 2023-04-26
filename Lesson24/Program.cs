using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace Lesson24;

internal static class Program
{
    private static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        // 1. Absolute path (C:/Windows/...)
        // 2. Relative path
        // "data.json"
        var exists = File.Exists("../../../data.json");
        var e2 = Directory.Exists("../../../obj/Debug");
        var persons = TextJsonSerialization.DeserializePersons();
        TextJsonSerialization.SerializePersons(persons);
        // XmlSerialization.SerializePersons(persons);
        
        // init + get the response + deserialize as model
        // try
        // {
        //     ProductModel[] result = await new HttpClient()
        //         .GetFromJsonAsync<ProductModel[]>("https://www.fruityvice.com/api/fruit/all");
        //     Console.ReadLine();
        // }
        // catch (HttpRequestException e)
        // {
        //     Console.WriteLine(e.Message);
        // }

        
        // init httpclient
        HttpClient httpClient = new HttpClient();
        // // get response
        // var resp = await httpClient.GetAsync("https://www.fruityvice.com/api/fruit/all");
        // // check for success
        // resp.EnsureSuccessStatusCode();
        // // get the content as string
        // string content = await resp.Content.ReadAsStringAsync();
        // // get the content as class objects
        // ProductModel[] products = await resp.Content.ReadFromJsonAsync<ProductModel[]>();
    }

    private static void PLinqTest()
    {
        // Створіть приклад колекції
        List<int> numbers = new List<int> {23, 12, 45, 67, 89, 30, 5, 14};
        // Використовуйте PLINQ для упорядкування колекції паралельно
        var orderedNumbers = numbers.AsParallel().OrderBy(n => n).ToList();
        // Надрукуйте відсортовану колекцію
        Console.WriteLine("Упорядкована колекція:");
        foreach (var number in orderedNumbers)
        {
            Console.WriteLine(number);
        }
    }

    private static void TplTest()
    {
        Console.OutputEncoding = Encoding.UTF8;
        // Створення колекції цілих чисел
        List<int> numbers = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        // Створення безпечної для потоків колекції для зберігання квадратів чисел
        ConcurrentBag<int> squares = new ConcurrentBag<int>();
        // Виконання паралельних операцій на кожному елементі колекції numbers
        Parallel.ForEach(numbers, number =>
        {
            Console.WriteLine($"Обробка числа {number} на потоці {Task.CurrentId}");
            int square = number * number;
            squares.Add(square);
        });
        Console.WriteLine("Квадрати чисел:");
        foreach (int square in squares)
        {
            Console.WriteLine(square);
        }
    }
}