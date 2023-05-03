using Microsoft.EntityFrameworkCore;

namespace Lesson26;

public sealed class SampleContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Person>()
            .HasOne(person => person.Order)
            .WithOne(order => order.Person)
            .HasForeignKey<Order>(order => order.PersonId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        // how to connect to the database
        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=Beetroot;" +
            "Integrated Security=True;");
}