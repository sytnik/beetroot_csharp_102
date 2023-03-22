namespace Lesson8;

public class Book
{
    public int Id { get; set; }
    // many
    public List<Author> Authors { get; set; }
    // many
    public List<Chapter> Chapters { get; set; }
}