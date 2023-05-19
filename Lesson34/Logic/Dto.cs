using Lesson34.Dao;

namespace Lesson34.Logic;

public record PersonDto(string FName, string LName, List<OrderDto> Orders);

public record OrderDto(string Info, string Address, List<Product> Products);