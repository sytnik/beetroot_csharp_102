namespace Lesson28.Models;

public sealed record Order : EntityWithId
{
    public Order(int id, int personId, string info)
    {
        Id = id;
        PersonId = personId;
        Info = info;
    }

    public int PersonId { get; set; }
    public string Info { get; set; }
    public Person Person { get; set; }
    public OrderDetails OrderDetails { get; set; }
    public List<Product> Products { get; set; }
}