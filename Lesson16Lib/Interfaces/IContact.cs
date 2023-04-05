namespace Lesson15Lib.Interfaces;

public interface IContact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string GetFullInfo();
}