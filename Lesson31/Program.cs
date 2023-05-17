// create the web application builder
var webApplicationBuilder = WebApplication.CreateBuilder(args);
webApplicationBuilder.Services.AddControllersWithViews()
    // needed for localization and validation
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
        options.HtmlHelperOptions.Html5DateRenderingMode =
            Microsoft.AspNetCore.Mvc.Rendering.Html5DateRenderingMode.CurrentCulture;
    })
    .AddDataAnnotationsLocalization()
    .AddMvcLocalization()
    .Services
    // needed for localization and validation
    .AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });
// register the database context
// add the HttpClient
webApplicationBuilder.Services.AddScoped(_=> new HttpClient());
webApplicationBuilder.Services.AddDbContext<SampleContext>();
// register the database context factory and build the web application
var webApplication = webApplicationBuilder.Build();
if (!webApplication.Environment.IsDevelopment())
{
    webApplication.UseExceptionHandler("/Home/Error");
    webApplication.UseHsts();
}

webApplication.UseHttpsRedirection();
webApplication.UseStaticFiles();
webApplication.UseRouting();
webApplication.UseAuthorization();
webApplication.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// create minimal APIs
ConfigureAPIs(webApplication);

webApplication.Run();

void ConfigureAPIs(WebApplication application)
{
    application.MapGet("/helloworld", () => "Hello World, lesson 32!");
    application.MapGet("/persons10", (SampleContext db) =>
        db.Persons.Take(10).ToList());
// api with parameters
    application.MapPost("/helloworld", (string id) => "Hello World, id: " + id);
// api with parameters and body
    application.MapPost("/helloworldwithbody", ([FromBody] string param) =>
        "Hello World, body param: " + param);

    // create api for person(s)
    application.MapGet("/person/list",
        (SampleContext context) =>
            context.Persons.ToList());
    // id has to be an integer
    // https://localhost:7079/person/1
    application.MapGet("/person/{id:int}",
        (int id, SampleContext context) =>
        {
            var person = context.Persons.Find(id);
            return person == null ? Results.NotFound() : Results.Ok(person);
        });
    application.MapPost("/person",
        (Person person, SampleContext context) =>
        {
            if (context.Persons.Any(p => p.Id == person.Id))
                return Results.BadRequest("Person already exists");
            context.Persons.Add(person);
            context.SaveChanges();
            return Results.Created($"/person/{person.Id}", person);
        });
    application.MapPut("/person",
        (Person person, SampleContext context) =>
        {
            if (!context.Persons.Any(p => p.Id == person.Id))
                return Results.BadRequest("Person is not updateable, because it not exists");
            var dbPerson = context.Persons.Find(person.Id);
            context.Entry(dbPerson).CurrentValues.SetValues(person);
            context.SaveChanges();
            return Results.Created($"/person/{person.Id}", person);
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
}