using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(UserReferenceNumberJsonConverter))]
public class UserReferenceNumber : RecordStructureBase
{
    public UserReferenceNumber() { }
    public UserReferenceNumber(Record record) : base(record) { }

    private string? userReferenceType = null;
    public string UserReferenceType => userReferenceType ??= GetValue(Tag.Type);

    public override string ToString() => $"{Record.Value}, {UserReferenceType}";
}

internal sealed class UserReferenceNumberJsonConverter : JsonConverter<UserReferenceNumber>
{
    public override UserReferenceNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, UserReferenceNumber value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new UserReferenceNumberJson(value), options);
    }
}

public class UserReferenceNumberJson(UserReferenceNumber userReferenceNumber) : GedcomJson
{
    public string? UserReferenceType { get; set; } = JsonString(userReferenceNumber.UserReferenceType);

    public override string ToString() => $"{UserReferenceType}";
}

#region USER_REFERENCE_TYPE (REFN) p. 27
/* 

n @
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64

*/
#endregion