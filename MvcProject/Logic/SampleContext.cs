using Lesson36.Dao;

namespace Lesson36.Logic;

public sealed class SampleContext : DbContext
{
    public DbSet<Admin> Admin { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<OrderProduct> OrderProduct { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // one to one
        builder.Entity<Order>()
            .HasOne(order => order.OrderDetails)
            .WithOne(details => details.Order)
            .HasForeignKey<OrderDetails>(details => details.OrderId);
        // one to many
        builder.Entity<Order>()
            .HasOne(order => order.Person)
            .WithMany(person => person.Orders)
            .HasForeignKey(order => order.PersonId);
        // many to many
        builder.Entity<Order>()
            .HasMany(order => order.Products)
            .WithMany(product => product.Orders)
            .UsingEntity<OrderProduct>(
                typeBuilder => typeBuilder
                    .HasOne(orderProduct => orderProduct.Product).WithMany()
                    .HasForeignKey(orderProduct => orderProduct.ProductId),
                typeBuilder => typeBuilder
                    .HasOne(orderProduct => orderProduct.Order).WithMany()
                    .HasForeignKey(orderProduct => orderProduct.OrderId),
                typeBuilder => typeBuilder
                    .HasKey(orderProduct =>
                        new {orderProduct.ProductId, orderProduct.OrderId}));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        // how to connect to the database
        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=Beetroot;" +
            "Integrated Security=True;");
}