using System.Text;
using Lesson17;
using Lesson17.SnakeGame;

Apple someApple = new Apple(new Position(5, 6));
List<Apple> apples = new List<Apple>
{
    new Apple(new Position(1, 3)),
    someApple,
    new Apple(new Position(10, 23)),
};

Predicate<Apple> pred = CoordinatesMoreThanFive;
Predicate<Apple> pred1 = apple =>
    apple.Position.Left > 5 && apple.Position.Top > 5;
Apple first = apples.Find(pred1);
Apple first1 = apples.Find(a =>
    a.Position.Left > 1 && a.Position.Top < 20);
// create a list of unique left positions, which are more than 1
// from the list of apple
List<int> coords = apples.Select(a => a.Position.Left)
    .Where(p => p > 1).Distinct().ToList();
bool second = apples.Contains(someApple);
int index = apples.IndexOf(someApple);
Console.ReadLine();

bool CoordinatesMoreThanFive(Apple apple)
    => apple.Position.Left > 5 && apple.Position.Top > 5;


// EventClass eventClass = new EventClass();
// EventSubscriber eventSubscriber = new EventSubscriber(eventClass);
// eventClass.DoSomething();

// Console.OutputEncoding = Encoding.UTF8;
// var game = new Game();
//
// using (var token = new CancellationTokenSource())
// {
//     var check = CheckKeyPresses(token);
//     do
//     {
//         game.OnTick();
//         game.Render();
//         await Task.Delay(game.GameRate);
//     } while (!Game.IsGameOver);
//
//     token.Cancel();
//     await check;
// }
//
// async Task CheckKeyPresses(CancellationTokenSource cancellationTokenSource)
// {
//     while (!cancellationTokenSource.Token.IsCancellationRequested)
//     {
//         if (Console.KeyAvailable) game.OnKeyPress(Console.ReadKey(true).Key);
//         await Task.Delay(100);
//     }
// }