using System.Security.Claims;
using Bogus;
using Lesson36.Dao;
using Lesson36.Logic;
using Lesson36.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Person = Lesson36.Dao.Person;

namespace Lesson36.Controllers;

// [LoggingFilter]
public partial class HomeController : Controller
{
    private readonly IStringLocalizer<HomeController> _localizer;
    private readonly SampleContext _context;
    public HomeController(SampleContext context, IStringLocalizer<HomeController> localizer)
    {
        _context = context;
        _localizer = localizer;
    }

    // [HttpGet, LoggingFilter]
    public IActionResult Index()
    {
        var str = _localizer["TestString"];
        var str2 = _localizer.GetString("TestString");
        return View();
    }

    // [Route("[action]")]
    // [Route("PrivacyPage")]
    // [Route("[controller]/[action]")]
    public IActionResult PrivacyPage() => View();

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
        var req = Request;
        var dbPerson = _context.Persons.Find(person.Id);
        _context.Entry(dbPerson).CurrentValues.SetValues(person);
        _context.SaveChanges();
        if(Request.Form.Files.Any()) UploadImage(person.Id, Request.Form.Files[0]);
        return RedirectToAction("Index");
    }

    public void UploadImage(int userId, IFormFile file)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "users",
            $"{userId}{Path.GetExtension(file.FileName)}");
        // ibrowserfile - only async, IFormFile both sync and async
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult ListPersons() => View();

    public IActionResult PersonsOrders() => View();

    // render the login form
    public IActionResult Login() =>
        View(new Admin {ReturnUrl = HttpContext.Request.Query["ReturnUrl"].ToString()});

    // login the user
    [HttpPost]
    public async Task<IActionResult> Login(Admin admin)
    {
        var hashedPassword = PasswordEncryption.HashPassword(admin.Pass);
        var dbAdmin = _context.Admin
            .Where(a => a.Login == admin.Login && a.Pass == hashedPassword)
            .Select(a => new AdminDto(a.Login, a.Role))
            .FirstOrDefault();
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

    public record AdminDto(string Login, string Role);

    // logout the user
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }

    public IActionResult ListOrders() => View();
    public IActionResult EditOrder(int id) => View(_context.Orders.Find(id));

    [HttpPost]
    public IActionResult EditOrder(Order order)
    {
        var dbOrder = _context.Orders.Find(order.Id);
        if (_context.Persons.Find(order.PersonId) == null)
            ModelState.AddModelError("personId",
                $"Person with Id={order.PersonId} specified not found");
        else
        {
            _context.Entry(dbOrder).CurrentValues.SetValues(order);
            _context.SaveChanges();
            return RedirectToAction("ListOrders");
        }

        return View(order);
    }

    public IActionResult HashAll()
    {
        var admins = _context.Admin.ToList();
        admins.ForEach(a => a.Pass = PasswordEncryption.HashPassword(a.Pass));
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}