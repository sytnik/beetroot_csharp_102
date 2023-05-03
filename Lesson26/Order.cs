namespace Lesson26;

public sealed record Order
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string Info { get; set; }
    public Person Person { get; set; }
}