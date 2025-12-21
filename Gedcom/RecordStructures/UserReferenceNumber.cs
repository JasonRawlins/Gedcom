
using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(UserReferenceNumberJsonConverter))]
public class UserReferenceNumber : RecordStructureBase
{
    public UserReferenceNumber() { }
    public UserReferenceNumber(Record record) : base(record) { }

    public string UserReferenceType => GetValue(Tag.Type);

    public override string ToString() => $"{Record.Value}, {UserReferenceType}";
}

internal class UserReferenceNumberJsonConverter : JsonConverter<UserReferenceNumber>
{
    public override UserReferenceNumber? ReadJson(JsonReader reader, Type objectType, UserReferenceNumber? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, UserReferenceNumber? userReferenceNumber, JsonSerializer serializer)
    {
        if (userReferenceNumber == null) throw new ArgumentNullException(nameof(userReferenceNumber));

        serializer.Serialize(writer, new UserReferenceNumberJson(userReferenceNumber));
    }
}

internal class UserReferenceNumberJson : GedcomJson
{
    public UserReferenceNumberJson(UserReferenceNumber userReferenceNumber)
    {
        UserReferenceType = JsonString(userReferenceNumber.UserReferenceType);
    }

    public string? UserReferenceType { get; set; }
}

#region USER_REFERENCE_TYPE (REFN) p. 27
/* 

n @
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64

*/
#endregion