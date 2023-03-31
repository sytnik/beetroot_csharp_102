namespace Lesson8;

public class Program
{
    public int SomeValue;

    public static void Main()
    {
        // BoxingUnboxingTest();
        Point p1 = new Point {X = 10, Y = 20};
        // p1.somenonstaticmethod();
        // Point.staticMethod();
        // Point p2 = new Point {X = 5, Y = 15};
        // Point p3 = p1 + p2;
        // p3 = p2 + 5;
        // Point p4 = Point.SubtractPoints(p1, p2);
        // MyCollection collection = new MyCollection();
        // collection[0] = 42;
        // collection[0] = 100;
        // int value = collection[0];
        // int value2 = collection[5];
        List<PersonDataAccessObjectDAO> dataBase = new List<PersonDataAccessObjectDAO>
        {
            new() {Id = 1, FirstName = "FirstName1", LastName = "LastName1", OtherInfo = "info1"},
            new() {Id = 2, FirstName = "FirstName2", LastName = "LastName2", OtherInfo = "info"},
            new() {Id = 3, FirstName = "FirstName3", LastName = "LastName3", OtherInfo = "info"},
            new() {Id = 4, FirstName = "FirstName4", LastName = "LastName4", OtherInfo = "info"},
            new() {Id = 5, FirstName = "FirstName5", LastName = "LastName5", OtherInfo = "info"},
        };

        // return collection (all users)
        // print firstname + lastname(0) + info
        var output = OutputData(dataBase);
    }

    public static PersonDTO[] OutputData(List<PersonDataAccessObjectDAO> dao)
    {
        PersonDTO[] res = new PersonDTO[dao.Count];
        // array -> for with index -> return
        // list -> foreach with add -> return list.ToArray()
        int index = 0;
        foreach (var person in dao)
        {
            res[index] = new PersonDTO(
                person.FirstName, person.LastName[0], person.OtherInfo);
            index++;
        }

        return res;
    }

    interface IInterface
    {
        public void DoSmth1();

        public void DoSmth()
        {
            Console.WriteLine("123");
        }
    }

    public class MyClass : IInterface
    {
        public void DoSmth1()
        {
            // DoSmth();
            throw new NotImplementedException();
        }
    }
    
    public readonly record struct PersonDTO(
        string FirstName, char LastName, string Info);

    public class PersonDataAccessObjectDAO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherInfo { get; set; }
    }

    public class MyCollection
    {
        // private int[] data = new int[10];

        private readonly List<int> _data;

        public MyCollection()
        {
            _data = new List<int> {5, 6, 7};
        }

        public int this[int index]
        {
            get => _data.ElementAtOrDefault(index);
            set
            {
                if (_data.Count > index) _data[index] = value;
                else _data.Add(value);
            }
        }

        public void SomeMethod()
        {
            // _data = new List<int>();
            _data.Clear();
        }
    }
    
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Point operator +(Point p1, Point p2)
            => new() {X = p1.X + p2.X, Y = p1.Y + p2.Y};

        public static Point operator +(Point p1, int val)
            => new() {X = p1.X + val, Y = p1.Y};

        public static Point operator *(Point p1, Point p2)
            => new() {X = p1.X * p2.X, Y = 0};

        public static Point SubtractPoints(Point p1, Point p2)
            => new() {X = p1.X - p2.X, Y = p1.Y - p2.Y};
    }
    
    static void BoxingUnboxingTest()
    {
        // boxing: value type -> object
        int val = 5;
        object obj = "qwe";
        // unboxing object -> value type
        string strRes = obj as string;
        int res = 0;
        // 1st option
        int.TryParse(obj.ToString(), out res);
        // unsafe case
        res = (int) obj;
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