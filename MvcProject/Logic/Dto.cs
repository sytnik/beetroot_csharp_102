using MvcProject.Dao;

namespace MvcProject.Logic;

public record PersonDto(string FName, string LName, List<OrderDto> Orders);

public record PersonApiDto(int Id, string FirstName, string LastName, int Age,
    string Gender, string Address);

public record OrderDto(string Info, string Address, List<Product> Products);

public record AdminDto(string Login, string Role);

public record ApiAdminDto(string Login, string Pass, string Role);