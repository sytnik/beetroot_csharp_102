namespace Lesson31.Controllers;

public class HomeController : Controller
{
    private readonly SampleContext _context;
    public HomeController(SampleContext context) => _context = context;

    [HttpGet]
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

    [Route("[action]")]
    public IActionResult ListPersons() => View();

    public IActionResult PersonsOrders() => View();
}