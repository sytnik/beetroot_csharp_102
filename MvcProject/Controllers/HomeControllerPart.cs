using Bogus;
using Lesson36.Dao;
using Person = Lesson36.Dao.Person;

namespace Lesson36.Controllers;

public partial class HomeController
{
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
}