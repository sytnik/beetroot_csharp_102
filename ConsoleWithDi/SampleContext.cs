namespace ConsoleWithDi;

public sealed class SampleContext : DbContext
{
    public DbSet<Admin> Admin { get; set; }
    public SampleContext(DbContextOptions<SampleContext> options) : base(options)
    {
    }
}