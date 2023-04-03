using Lesson15Lib.Interfaces;
using Newtonsoft.Json;

namespace Lesson15Lib.Persons;

public class Family : Person, IContact
{
    private string _relationship;

    [JsonProperty]
    private string relationship { get => _relationship; set => _relationship = value; }

    public string Relationship => _relationship;

    public Family(string phoneNumber, string lastName, string firstName, int relationship) : base(phoneNumber, lastName, firstName) => SetRelationship(relationship);

    public void SetRelationship(int relationship) => _relationship = relationship switch
    {
        1 => MyConstants.Relationship.WifeHusband.ToString(),
        2 => MyConstants.Relationship.Parents.ToString(),
        3 => MyConstants.Relationship.BrotherSister.ToString(),
        4 => MyConstants.Relationship.Chield.ToString(),
        5 => MyConstants.Relationship.GodParents.ToString(),
        _ => "Empty",
    };

    public new string GetFullInfo() => $"Full name:\t{LastName,2} {FirstName,2}\nRelationship:\t{Relationship}\nPhone number:\t{PhoneNumber,2}";
}