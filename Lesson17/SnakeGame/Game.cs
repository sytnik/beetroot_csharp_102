namespace Lesson17.SnakeGame;

public class Game
{
    public Direction CurrentDirection;
    public Direction NextDirection;
    public readonly Snake Snake;
    private readonly Position _startPosition = new(1, 0);
    private Apple _apple;
    private int _rate = 120;
    public TimeSpan GameRate => TimeSpan.FromMilliseconds(_rate);

    public Game()
    {
        Snake = new Snake(_startPosition, 3);
        _apple = GameExtensions.CreateApple();
        CurrentDirection = Direction.Right;
        NextDirection = Direction.Right;
    }

    public static bool IsGameOver => !Snake.IsAlive;

    public void OnKeyPress(ConsoleKey key)
    {
        var newDirection = CurrentDirection;
        switch (key)
        {
            case ConsoleKey.UpArrow:
                newDirection = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                newDirection = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                newDirection = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                newDirection = Direction.Right;
                break;
            case ConsoleKey.End:
                _rate = _rate > 10 ? _rate - 10 : _rate;
                break;
            case ConsoleKey.Delete:
                _rate = _rate < 800 ? _rate + 10 : _rate;
                break;
            default: return;
        }
        // -> <-
        if (newDirection == CurrentDirection.OppositeDirection()) return;
        NextDirection = newDirection;
    }

    public void Render()
    {
        Console.Clear();
        Snake.Render(this);
        _apple.Render();
        Console.SetCursorPosition(0, 0);
    }

    public void OnTick()
    {
        if (IsGameOver) throw new Exception();
        CurrentDirection = NextDirection;
        Snake.Move(CurrentDirection);
        if (Snake.Head.Equals(_apple.Position))
        {
            Snake.Grow();
            _apple = GameExtensions.CreateApple();
        }
    }
}