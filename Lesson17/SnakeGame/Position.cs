namespace Lesson17.SnakeGame;

public readonly struct Position
{
    public int Top { get; }
    public int Left { get; }

    public Position(int top, int left)
    {
        Top = top;
        Left = left;
    }

    public Position RightBy(int n) => new(Top, Left + n);
    public Position DownBy(int n) => new(Top + n, Left);
}