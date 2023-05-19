using System.Security.Claims;
using Lesson34.Dao;
using Lesson34.Logic;
using Lesson34.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Lesson34.Controllers;

// [LoggingFilter]
public class HomeController : Controller
{
    private readonly SampleContext _context;
    public HomeController(SampleContext context) => _context = context;

    // [HttpGet, LoggingFilter]
    public IActionResult Index() => View();

    // [Route("[action]")]
    [Route("PrivacyPage")]
    // [Route("[controller]/[action]")]
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel
        {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});

    // display person add form
    public IActionResult CreatePerson() => View();

    [HttpPost]
    public IActionResult CreatePerson(Person person)
    {
        var newId = _context.Persons.Max(p => p.Id) + 1;
        _context.Persons.Add(person with {Id = newId});
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // get person for edit form
    [HttpGet]
    public IActionResult EditPerson(int id) => View(_context.Persons.Find(id));

    [HttpPost]
    public IActionResult EditPerson(Person person)
    {
        var dbPerson = _context.Persons.Find(person.Id);
        _context.Entry(dbPerson).CurrentValues.SetValues(person);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [Route("[action]"), Authorize(Roles = "Admin")]
    public IActionResult ListPersons() => View();

    public IActionResult PersonsOrders() => View();

    // render the login form
    public IActionResult Login() =>
        View(new Admin {ReturnUrl = HttpContext.Request.Query["ReturnUrl"].ToString()});

    // login the user
    [HttpPost]
    public async Task<IActionResult> Login(Admin admin)
    {
        var dbAdmin = _context.Admin.FirstOrDefault(a =>
            a.Login == admin.Login && a.Pass == admin.Pass);
        if (dbAdmin == null) return RedirectToAction("Login");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, dbAdmin.Login),
                    new(ClaimTypes.Role, dbAdmin.Role)
                }, CookieAuthenticationDefaults.AuthenticationScheme)));
        if (!string.IsNullOrWhiteSpace(admin.ReturnUrl) && Url.IsLocalUrl(admin.ReturnUrl))
            return Redirect(admin.ReturnUrl);
        return RedirectToAction("Index");
    }

    // logout the user
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }
}