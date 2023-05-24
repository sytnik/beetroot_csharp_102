namespace Lesson36.Dao;

public class Admin
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Pass { get; set; }
    public string Role { get; set; }
    
    [NotMapped]
    public string ReturnUrl { get; set; }
}