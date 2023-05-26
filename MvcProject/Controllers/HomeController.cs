using System.Security.Claims;
using Bogus;
using Lesson36.Dao;
using Lesson36.Logic;
using Lesson36.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Person = Lesson36.Dao.Person;

namespace Lesson36.Controllers;

// [LoggingFilter]
public class HomeController : Controller
{
    private readonly SampleContext _context;
    public HomeController(SampleContext context) => _context = context;

    // [HttpGet, LoggingFilter]
    public IActionResult Index() => View();

    // [Route("[action]")]
    // [Route("PrivacyPage")]
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

    public IActionResult FillTables()
    {
        // create faker
        var faker = new Faker<Person>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Age, f => f.Random.Int(18, 100))
            .RuleFor(p => p.Gender, f => f.Person.Gender.ToString())
            .RuleFor(p => p.Address, f => f.Person.Phone)
            .RuleFor(p => p.Address, f => f.Address.FullAddress());
        // generate 100 persons
        var persons = faker.Generate(100);
        // set the ids starting from the max in persons table
        var maxId = _context.Persons.Max(p => p.Id);
        // foreach (var p in persons)
        // {
        //     p.Id = ++maxId;
        // }
        persons.ForEach(p => p.Id = ++maxId);
        _context.Persons.AddRange(persons);
        // create faker for products
        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 100000));
        maxId = _context.Products.Max(p => p.Id);
        var products = productFaker.Generate(100);
        products.ForEach(p => p.Id = ++maxId);
        _context.Products.AddRange(products);
        // create faker for orders
        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.PersonId, f => f.PickRandom(persons.Select(p => p.Id)))
            .RuleFor(o => o.Info, f => f.Lorem.Sentences());
        maxId = _context.Orders.Max(p => p.Id);
        var orders = orderFaker.Generate(100);
        orders.ForEach(p => p.Id = ++maxId);
        _context.Orders.AddRange(orders);
        // create faker for order details
        var orderDetailsFaker = new Faker<OrderDetails>()
            .RuleFor(o => o.OrderId, f => f.PickRandom(orders.Select(p => p.Id)))
            .RuleFor(o => o.ShippingAddress, f => f.Address.FullAddress());
        maxId = _context.OrderDetails.Max(p => p.Id);
        var detailsList = orderDetailsFaker.Generate(100);
        detailsList.ForEach(od => od.Id = ++maxId);
        _context.OrderDetails.AddRange(detailsList);
        // create faker for order products
        var orderProductsFaker = new Faker<OrderProduct>()
            .RuleFor(op => op.OrderId, f => f.PickRandom(orders.Select(p => p.Id)))
            .RuleFor(op => op.ProductId, f => f.PickRandom(products.Select(p => p.Id)));
        var orderProducts = orderProductsFaker.Generate(100);
        var orderProductsDb = _context.OrderProduct.ToList();
        orderProducts = orderProducts.Except(orderProductsDb).ToList();
        _context.OrderProduct.AddRange(orderProducts);
        _context.SaveChanges();
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
}