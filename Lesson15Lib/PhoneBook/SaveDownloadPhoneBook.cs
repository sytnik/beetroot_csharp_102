using System.Text.Json;

namespace Lesson15Lib.PhoneBook;

public class SaveDownloadPhoneBook
{
    public static void Serialize(PhoneBook phoneBook)
    {
        using var fileStream = new FileStream("MyPhoneBook.json", FileMode.OpenOrCreate);
        JsonSerializer.Serialize(fileStream, phoneBook);
    }

    public static PhoneBook Deserialize()
    {
        using var fileStream = new FileStream("MyPhoneBook.json", FileMode.OpenOrCreate);
        return JsonSerializer.Deserialize<PhoneBook>(fileStream);
    }
}