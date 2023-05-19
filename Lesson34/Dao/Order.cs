namespace Lesson34.Dao;

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
    // load details
    public OrderDetails OrderDetails { get; set; }
    // load products
    public List<Product> Products { get; set; }
}