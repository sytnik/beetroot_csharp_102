using Lesson15Lib.Exceptions;
using Lesson15Lib.Interfaces;

namespace Lesson15Lib.Persons;

public class Person : IContact
{
    private string _firstName;
    private string _lastName;
    private string _phoneNumber;
    
    public string FirstName
    {
        get => _firstName;
        set => _firstName = PersonInputValuesChecker.CheckName(value, nameof(FirstName));
    }
    public string LastName
    {
        get => _lastName;
        set => _lastName = PersonInputValuesChecker.CheckName(value, nameof(LastName));
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = PersonInputValuesChecker.CheckPhoneNumber(value, nameof(PhoneNumber));
    }

    public Person(string phoneNumber, string lastName, string firstName)
    {
        if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phoneNumber))
        {
            throw new PersonException("Value is null or empty");
        }
        try
        {
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
        }
        catch (PersonException personException)
        {
            throw personException;
        }
    }

    public string GetFullInfo() => $"Full name:\t{LastName,2} {FirstName,2}\nPhone number:\t{PhoneNumber,2}";
}