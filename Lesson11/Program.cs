namespace Lesson8;

public class Program
{
    public int SomeValue;

    public static void Main()
    {
        List<Order> orders = new List<Order>();

        Order order = new Order();
        order.Id = 1;
        order.Date = DateTime.Now;

        Order order1 = new Order
        {
            Id = 2,
            Date = DateTime.Now.AddMinutes(30)
        };

        order.SaveOrder(orders);

        List<Shape> shapes = new List<Shape>
        {
            new Circle {Radius = 5},
            new Rectangle {Width = 2, Height = 7},
            new Rectangle {Width = 4, Height = 4}
        };
        AreaCalculator calc = new AreaCalculator();
        double sum = Math.Round(calc.TotalArea(shapes.ToArray()), 2);
    }
}

public interface IImage
{
    void Display();
}

public class RealImage : IImage
{
    private readonly string _filename;

    public RealImage(string filename)
    {
        _filename = filename;
        LoadFromDisk();
    }

    public void Display() => Console.WriteLine("Displaying " + _filename);

    private void LoadFromDisk() => Console.WriteLine("Loading " + _filename);
}

public class ProxyImage : IImage
{
    private readonly string _filename;
    private RealImage _realImage;

    public ProxyImage(string filename) => _filename = filename;

    public void Display()
    {
        if (_realImage == null)
        {
            _realImage = new RealImage(_filename);
        }

        _realImage.Display();
    }
}

public class Client
{
    private readonly IImage _image;

    public Client(IImage image) => _image = image;

    public void DisplayImage() => _image.Display();
}

public interface IShape
{
    double Area();
}

public class Circle2 : IShape
{
    public double Radius { get; set; }

    public double Area()
    {
        return Math.PI * Math.Pow(Radius, 2);
    }
}

public abstract class Shape
{
    public abstract double Area();

    public string OutputShape()
    {
        return "some shape data";
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public override double Area()
    {
        string data = OutputShape();
        return Math.PI * Math.Pow(Radius, 2);
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override double Area()
    {
        return Width * Height;
    }
}

public class AreaCalculator
{
    // area can be
    // circle
    // square
    // any other type with Area()
    public double TotalArea(Shape[] shapes)
    {
        double area = 0;

        foreach (var shape in shapes)
        {
            area += shape.Area();
        }

        return area;
    }
}

public interface IOrder
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string CustomerName { get; set; }
}

public class Order : IOrder
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

    public void SaveOrder(List<Order> userOrders)
    {
        if (Id > 0) userOrders.Add(this);
    }
}

public class OrderNotification
{
    // send via viber/tg etc.
    // different order types
    public void SendEmail(IOrder order)
    {
        Console.WriteLine($"Hello {order.CustomerName}," +
                          $" your order {order.Id} has been submitted");
    }
}