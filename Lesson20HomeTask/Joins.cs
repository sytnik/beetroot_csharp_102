namespace Lesson19HomeTask;

public class Joins
{
    public void JoinEntities()
    {
        List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "user1"
            }
        };
        List<Order> orders = new List<Order>
        {
            new Order{Id = 1, CustomerId = 1}
        };

        var query = customers.Join(
            orders,
            customer => customer.Id,
            order => order.CustomerId,
            (customer, order) => new {Customer = customer, Order = order}
        ).ToList();

        foreach (var result in query)
            Console.WriteLine($"Клієнт {result.Customer.Name} має замовлення з ID {result.Order.Id}");
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
}