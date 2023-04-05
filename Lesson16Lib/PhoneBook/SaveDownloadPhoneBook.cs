namespace Lesson15Lib.PhoneBook;

public class SaveDownloadPhoneBook
{
    private static readonly JsonSerializerSettings Settings = new()
        {TypeNameHandling = TypeNameHandling.Auto};

    public static void Serialize(PhoneBook phoneBook)
    {
        var json = JsonConvert.SerializeObject(phoneBook, Settings);
        File.WriteAllText(FilePath, json);
    }

    public static PhoneBook Deserialize()
    {
        var text = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<PhoneBook>(text, Settings);
    }
}