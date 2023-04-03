using Lesson15Lib.Interfaces;
using Lesson15Lib.Persons;

namespace Lesson15Lib.PhoneBook;

public class PhoneBook
{
    private List<Person> _contacts = new();

    public List<Person> Contacts
    {
        get => _contacts;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _contacts = value;
        }
    }

    public PhoneBook(){}
    public PhoneBook(params Person[] contacts)
    {
        foreach (var contact in contacts) Contacts.Add(contact);
        Sort();
    }

    public PhoneBook(PhoneBook phoneBook)
    {
        ArgumentNullException.ThrowIfNull(phoneBook);
        Contacts = phoneBook.Contacts;
        Sort();
    }

    public PhoneBook(List<Person> contact)
    {
        Contacts = contact;
        Sort();
    }

    public PhoneBook(Person contact)
    {
        Contacts.Add(contact);
        Sort();
    }

    public void AddContact(Person contact)
    {
        ArgumentNullException.ThrowIfNull(contact);
        Contacts.Add(contact);
        Sort();
    }

    public void AddContact(List<Person> contacts)
    {
        foreach (var contact in contacts)
        {
            AddContact(contact);
            Sort();
        }
    }

    public void AddContact(PhoneBook phoneBook)
    {
        foreach (var contact in phoneBook.Contacts) AddContact(contact);
        Sort();
    }

    public void RemoveContact(Person contact)
    {
        ArgumentNullException.ThrowIfNull(contact);
        Contacts.Remove(contact);
    }

    private void Sort()
    {
        if (Contacts.Count > 1)
            Contacts = Contacts.OrderBy(contact => contact.LastName).ThenBy(contact => contact.FirstName)
                .ThenBy(contact => contact.PhoneNumber).ToList();
    }
}