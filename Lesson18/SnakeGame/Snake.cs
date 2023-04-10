namespace Lesson17.SnakeGame;

public class Snake
{
    private readonly List<Position> _wholeBody;
    public Position Head => _wholeBody.First();
    public IEnumerable<Position> Body => _wholeBody.Skip(1);
    private int _growthRemaining;

    public static bool IsAlive => true;

    public Snake(Position location, int size = 1)
    {
        _wholeBody = new List<Position> {location};
        _growthRemaining = Math.Max(0, size - 1);
    }

    public void Move(Direction direction)
    {
        if (!IsAlive) throw new Exception("");
        var newHead = direction switch
        {
            Direction.Up => Head.DownBy(-1),
            Direction.Down => Head.DownBy(1),
            Direction.Left => Head.RightBy(-1),
            Direction.Right => Head.RightBy(1),
            _ => throw new Exception(""),
        };
        _wholeBody.Insert(0, newHead);
        if (_growthRemaining > 0) _growthRemaining--;
        else _wholeBody.RemoveAt(_wholeBody.Count - 1);
    }

    public void Grow()
    {
        if (!IsAlive) throw new Exception();
        _growthRemaining++;
    }
}