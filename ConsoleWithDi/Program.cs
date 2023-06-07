var s = ConfigureServices();
using var context = s.GetRequiredService<SampleContext>();
var admins = GetAdminsQueryable(context);
var admin = GetConcreteAdmin(admins);
Console.WriteLine(admin.Login);

static ServiceProvider ConfigureServices()
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    var collection = new ServiceCollection();
    collection.AddDbContext<SampleContext>(builder => builder.UseSqlServer(connectionString));
    return collection.BuildServiceProvider();
}

IQueryable<Admin> GetAdminsQueryable(SampleContext sampleContext) => sampleContext.Admin;
Admin GetConcreteAdmin(IQueryable<Admin> queryable) => queryable.FirstOrDefault();