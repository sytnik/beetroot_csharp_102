using var context = ConfigureServices().GetRequiredService<SampleContext>();
var admins = GetAdminsQueryable(context);
var admin = GetConcreteAdmin(admins);
Console.WriteLine(admin.Login);

static ServiceProvider ConfigureServices()
{
    var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true).Build();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    var collection = new ServiceCollection();
    collection.AddDbContext<SampleContext>(b => b.UseSqlServer(connectionString), ServiceLifetime.Transient);
    return collection.BuildServiceProvider();
}

IQueryable<Admin> GetAdminsQueryable(SampleContext sampleContext) => sampleContext.Admin;
Admin GetConcreteAdmin(IQueryable<Admin> queryable) => queryable.FirstOrDefault();