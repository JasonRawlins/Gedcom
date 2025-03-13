using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(UserReferenceNumberJsonConverter))]
public class UserReferenceNumber : RecordStructureBase
{
    public UserReferenceNumber() { }
    public UserReferenceNumber(Record record) : base(record) { }

    public string UserReferenceType => _(C.TYPE);
}

internal class UserReferenceNumberJsonConverter : JsonConverter<UserReferenceNumber>
{
    public override UserReferenceNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, UserReferenceNumber userReferenceNumber, JsonSerializerOptions options)
    {
        var userReferenceNumberJson = new UserReferenceNumberJson(userReferenceNumber);
        JsonSerializer.Serialize(writer, userReferenceNumberJson, userReferenceNumberJson.GetType(), options);
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