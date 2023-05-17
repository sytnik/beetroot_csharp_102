namespace Lesson31.Logic;

public record PersonDto(string FName, string LName, List<OrderDto> Orders);

public record OrderDto(string Info, string Address, List<Product> Products);