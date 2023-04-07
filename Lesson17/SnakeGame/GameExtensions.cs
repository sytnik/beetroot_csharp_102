namespace Lesson17.SnakeGame;

public static class GameExtensions
{
    public static Apple CreateApple()
    {
        var rows = 20;
        var columns = 20;
        var random = new Random();
        var top = random.Next(0, rows + 1);
        var left = random.Next(0, columns + 1);
        return new Apple(new Position(top, left));
    }

    public static void Render(this Apple apple)
    {
        Console.SetCursorPosition(apple.Position.Left, apple.Position.Top);
        Console.Write("🍏");
    }

    public static Direction OppositeDirection(this Direction direction) =>
        direction switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => throw new Exception()
        };

    public static void Render(this Snake snake, Game game)
    {
        game.RotateSnakeIfBounds();
        Console.SetCursorPosition(snake.Head.Left, snake.Head.Top);
        Console.Write("😃");
        foreach (var elem in snake.Body)
        {
            Console.SetCursorPosition(elem.Left, elem.Top);
            Console.Write("🧊");
        }
    }

    private static void RotateSnakeIfBounds(this Game game)
    {
        if (game.Snake.Head.Left == Console.WindowWidth - 6 || game.Snake.Head.Left == 0 ||
            game.Snake.Head.Top == Console.WindowHeight - 6 || game.Snake.Head.Top == 0)
            game.NextDirection = game.CurrentDirection.OppositeDirection();
    }
}