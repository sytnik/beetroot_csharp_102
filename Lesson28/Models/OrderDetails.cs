namespace Lesson28.Models;

public sealed record OrderDetails : EntityWithId
{
    public OrderDetails(int id, int orderId, string shippingAddress)
    {
        Id = id;
        OrderId = orderId;
        ShippingAddress = shippingAddress;
    }

    public string ShippingAddress { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}