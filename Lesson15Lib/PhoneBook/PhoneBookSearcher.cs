using Lesson15Lib.Exceptions;
using Lesson15Lib.Interfaces;
using Lesson15Lib.Persons;

namespace Lesson15Lib.PhoneBook;

public class PhoneBookSearcher
{
    public List<Person> SearchResult { get; private set; }

    public PhoneBookSearcher(string input, PhoneBook phonebook)
    {
        try
        {
            SearchContact(input, phonebook);
        }
        catch (PhoneBookSearchException phoneBookSearchException)
        {
            throw phoneBookSearchException;
        }
    }

    public void SearchContact(string input, PhoneBook phoneBook)
    {
        if (phoneBook == null || phoneBook.Contacts.Count < 1 || string.IsNullOrWhiteSpace(input))
            throw new PhoneBookSearchException();
        SearchResult = phoneBook.Contacts.Where(contact =>
                contact.FirstName.Contains(input, StringComparison.OrdinalIgnoreCase) ||
                contact.LastName.Contains(input, StringComparison.OrdinalIgnoreCase) ||
                contact.PhoneNumber.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}