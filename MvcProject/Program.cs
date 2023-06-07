using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using MvcProject;
using MvcProject.Dao;
using MvcProject.Logic;

// create the web application builder
var webApplicationBuilder = WebApplication.CreateBuilder(args);
// get connection string from appsettings.json
var isDevelopment = webApplicationBuilder.Environment.IsDevelopment();
// 1st part
webApplicationBuilder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
webApplicationBuilder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("uk-UA") };
    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// add authentication
webApplicationBuilder.Services
    .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/Home/Login");
webApplicationBuilder.Services.AddControllersWithViews();
// register the HttpClient
webApplicationBuilder.Services.AddScoped(_ => new HttpClient());
// register the database context
webApplicationBuilder.Services.AddDbContext<SampleContext>(builder =>
    builder.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")));
// webApplicationBuilder.Services.AddDbContext<SampleContext>();
// add the hosted service
webApplicationBuilder.Services.AddHostedService<AppHostedService>();
// for the api documentation
webApplicationBuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
// register the database context factory and build the web application
var webApplication = webApplicationBuilder.Build();
if (!webApplication.Environment.IsDevelopment())
{
    webApplication.UseExceptionHandler("/Home/Error");
}
// middleware for localization
var locOptions = webApplication.Services.GetService<IOptions<RequestLocalizationOptions>>();
webApplication.UseRequestLocalization(locOptions.Value);

webApplication.UseHsts();
webApplication.UseHttpsRedirection();
webApplication.UseStaticFiles();
webApplication.UseRouting();
// user authentication and authorization
webApplication.UseAuthentication();
webApplication.UseAuthorization();
webApplication.UseSwagger().UseSwaggerUI();
webApplication.MapDefaultControllerRoute();
// create minimal APIs
ConfigureAPIs(webApplication);
webApplication.Run();

void ConfigureAPIs(IEndpointRouteBuilder application)
{
    application.MapGet("/helloworld", () => "Hello World, lesson 32!");
    application.MapGet("/persons10", (SampleContext db) => db.Persons.Take(10).ToList());
    // api with parameters
    application.MapPost("/helloworld", (string id) => "Hello World, id: " + id);
    // api with parameters and body
    application.MapPost("/helloworldwithbody", ([FromBody] string param) => "Hello World, body param: " + param);
    // create api for person(s)
    application.MapGet("/person/list", (SampleContext context) => context.Persons.ToList());
    // id has to be an integer
    // https://localhost:7079/person/1
    application.MapGet("/person/{id:int}",
        (int id, SampleContext context) =>
        {
            var person = context.Persons.Find(id);
            return person == null ? Results.NotFound() : Results.Ok(person);
        });
    application.MapPost("/person", (PersonApiDto entity, SampleContext context) =>
    {
        if (context.Persons.Any(person => person.Id == entity.Id))
            return Results.BadRequest("Person already exists");
        var dbPerson = new Person(entity.Id, entity.FirstName, entity.LastName, entity.Age,
            entity.Gender, entity.Address);
        context.Persons.Add(dbPerson);
        context.SaveChanges();
        return Results.Created($"/person/{dbPerson.Id}", dbPerson);
    });
    application.MapPut("/person",
        (Person entity, SampleContext context) =>
        {
            if (!context.Persons.Any(person => person.Id == entity.Id))
                return Results.BadRequest("Person is not updateable, because it not exists");
            var dbPerson = context.Persons.Find(entity.Id);
            context.Entry(dbPerson!).CurrentValues.SetValues(entity);
            context.SaveChanges();
            return Results.Created($"/person/{entity.Id}", entity);
        });
    // https://localhost:7079/person/?id=1
    application.MapDelete("/person",
        (int id, SampleContext context) =>
        {
            var person = context.Persons.Find(id);
            if (person == null) return Results.NotFound("No person found");
            context.Persons.Remove(person);
            context.SaveChanges();
            return Results.Ok("Person deleted");
        });
    
    // simple approach
    application.MapPost("/admin", (Admin admin, SampleContext context) =>
    {
        if (context.Admin.Any(a => a.Id == admin.Id))
            return Results.BadRequest("Admin already exists");
        admin.Id = 0;
        admin.Pass = PasswordEncryption.HashPassword(admin.Pass);
        context.Admin.Add(admin);
        context.SaveChanges();
        return Results.Created($"/person/{admin.Id}", admin);
    });
    
    // right approach
    application.MapPost("/admindto", (AdminDto admin, SampleContext context) =>
    {
        context.Admin.Add(new Admin(admin.Login, PasswordEncryption.HashPassword(admin.Pass), admin.Role));
        context.SaveChanges();
        var first = context.Admin.First(a => a.Login == admin.Login);
        return Results.Created($"/person/{first.Id}", first);
    });
    
    application.MapGet("/admin/{id:int}",
        (int id, SampleContext context) =>
        {
            var admin = context.Admin.Find(id);
            return admin == null ? Results.NotFound() : Results.Ok(admin);
        });
    
    application.MapPut("/admin",
        (Admin admin, SampleContext context) =>
        {
            if (!context.Admin.Any(a => a.Id == admin.Id))
                return Results.BadRequest("Admin is not updateable, because it not exists");
            context.Entry(admin).State = EntityState.Modified;
            // var dbPerson = context.Persons.Find(admin.Id);
            // context.Entry(dbPerson!).CurrentValues.SetValues(admin);
            context.SaveChanges();
            return Results.Created($"/person/{admin.Id}", admin);
        });
    
}

namespace MvcProject
{
    record AdminDto(string Login, string Pass, string Role);

// just for integration tests
    public partial class Program
    {
    }
}