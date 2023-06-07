namespace MvcProject.Dao;

public sealed record Product : EntityWithId
{
    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public Product()
    {
    }

    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<Order> Orders { get; set; }
}