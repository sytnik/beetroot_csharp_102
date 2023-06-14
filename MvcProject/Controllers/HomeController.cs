using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using MvcProject.Dao;
using MvcProject.Logic;
using MvcProject.Models;
using Person = MvcProject.Dao.Person;

namespace MvcProject.Controllers;

public partial class HomeController : Controller
{
    private readonly IStringLocalizer<HomeController> _localizer;
    private readonly SampleContext _context;

    public HomeController(SampleContext context, IStringLocalizer<HomeController> localizer)
    {
        _context = context;
        _localizer = localizer;
    }
    
    public IActionResult Index()
    {
        GetSomeData();
        ViewBag.TestString = _localizer["TestString"];
        return View();
    }

    private void GetSomeData()
    {
        var total = _context.Orders
            .Where(o => o.OrderProducts.Any())
            .Take(1)
            .Select(o => o.OrderProducts.Sum(op => op.Count * op.Product.Price))
            .FirstOrDefault();
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
    public async Task<IActionResult> CreatePerson(Person person)
    {
        var newId = await _context.Persons.MaxAsync(p => p.Id) + 1;
        await _context.Persons.AddAsync(person with {Id = newId});
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions {Expires = DateTimeOffset.UtcNow.AddYears(1)}
        );

        return LocalRedirect(returnUrl);
    }

    // get person for edit form
    [Authorize(Roles = "Admin")]
    public IActionResult EditPerson(int id) => View(_context.Persons.Find(id));

    [HttpPost]
    public IActionResult EditPerson(Person person)
    {
        var req = Request;
        var dbPerson = _context.Persons.Find(person.Id);
        _context.Entry(dbPerson).CurrentValues.SetValues(person);
        _context.SaveChanges();
        if (Request.Form.Files.Any()) UploadImage(person.Id, Request.Form.Files[0]);
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
        // if (_context.Admin.Any(a => a.Login == admin.Login))
        // {
        //     ModelState.AddModelError("Login",
        //         $"Admin with Login{admin.Login} already exists");
        //     return View(admin);
        // }

        var hashedPassword = PasswordEncryption.HashPassword(admin.Pass);
        var dbAdmin = _context.Admin
            .Where(a => a.Login == admin.Login && a.Pass == hashedPassword)
            .Select(a => new AdminDto(a.Login, a.Role))
            .FirstOrDefault();
        if (dbAdmin == null) return RedirectToAction("Login");
        await SignIn(HttpContext, dbAdmin);
        if (!string.IsNullOrWhiteSpace(admin.ReturnUrl) && Url.IsLocalUrl(admin.ReturnUrl))
            return Redirect(admin.ReturnUrl);
        return RedirectToAction("Index");
    }

    private static async Task SignIn(HttpContext context, AdminDto dbAdmin)
    {
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, dbAdmin.Login),
                    new(ClaimTypes.Role, dbAdmin.Role)
                }, CookieAuthenticationDefaults.AuthenticationScheme)));
    }

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