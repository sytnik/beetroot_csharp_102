using Lesson15Lib.Exceptions;
using Lesson15Lib.Interfaces;

namespace Lesson15Lib.Persons;

public class Collegue : Person, IContact
{
    private string _company;
    
    public string Company
    {
        get => _company;
        set => _company = PersonInputValuesChecker.CheckName(value, nameof(Company));
    }

    public Collegue(string phoneNumber, string lastName, string firstName, string company) : base(phoneNumber,
        lastName, firstName)
    {
        try
        {
            Company = company;
        }
        catch (PersonException personException)
        {
            throw personException;
        }
    }

    public new string GetFullInfo() =>
        $"Full name:\t{LastName,2} {FirstName,2}\nCompany:\t{Company}\nPhone number:\t{PhoneNumber,2}";
}