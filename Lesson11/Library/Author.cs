// projectname.catalogname
namespace Lesson8.Library;

public class Author
{
    public int Id { get; set; }
    public Biography Biography { get; set; }
    public string Name { get; set; }
    // many books
    public List<Book> Books { get; set; }

    public Author(List<Book> books)
    {
        Books = books;
    }
}