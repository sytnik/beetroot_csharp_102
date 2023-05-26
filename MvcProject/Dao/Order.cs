namespace Lesson36.Dao;

public sealed record Order : EntityWithId
{
    public Order(int id, int personId, string info)
    {
        Id = id;
        PersonId = personId;
        Info = info;
    }

    public Order()
    {
    }

    public int PersonId { get; set; }

    [Required(ErrorMessage = "Info is required.")]
    [MinLength(10, ErrorMessage = "Info must be at least 10 characters long.")]
    [StringLength(200, ErrorMessage = "Info cannot be longer than 200 characters.")]
    public string? Info { get; set; }

    public Person Person { get; set; }

    // load details
    public OrderDetails OrderDetails { get; set; }

    // load products
    public List<Product> Products { get; set; }
}