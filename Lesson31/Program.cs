using Lesson31.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews()
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
builder.Services.AddDbContext<SampleContext>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();