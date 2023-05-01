using Microsoft.EntityFrameworkCore;

namespace Lesson26;

public class SampleContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        // how to connect to the database
        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=Beetroot;" +
            "Integrated Security=True;");
}