using System.Text;
using Lesson17.SnakeGame;

Console.OutputEncoding = Encoding.UTF8;
var game = new Game();

using (var token = new CancellationTokenSource())
{
    var check = CheckKeyPresses(token);
    do
    {
        game.OnTick();
        game.Render();
        await Task.Delay(game.GameRate);
    } while (!Game.IsGameOver);

    token.Cancel();
    await check;
}

async Task CheckKeyPresses(CancellationTokenSource cancellationTokenSource)
{
    while (!cancellationTokenSource.Token.IsCancellationRequested)
    {
        if (Console.KeyAvailable) game.OnKeyPress(Console.ReadKey(true).Key);
        await Task.Delay(100);
    }
}