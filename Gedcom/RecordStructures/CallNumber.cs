using System.Text.Json;
using System.Text.Json.Serialization;
using Gedcom.Core;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(CallNumberJsonConverter))]
public class CallNumber : RecordStructureBase
{
    public CallNumber() : base() { }
    public CallNumber(Record record) : base(record) { }

    public string SourceMediaType => GetValue(Tag.Media);

    public override string ToString() => $"{Record.Value}, {SourceMediaType}";
}

internal sealed class CallNumberJsonConverter : JsonConverter<CallNumber>
{
    public override CallNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, CallNumber value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new CallNumberJson(value), options);
    }
}

public class CallNumberJson(CallNumber callNumber) : GedcomJson
{
    public string? SourceMediaType { get; set; } = JsonString(callNumber.SourceMediaType);
    public override string ToString() => $"{SourceMediaType}";
}

#region CALL_NUMBER p. 40
/* 

CALN {CALL_NUMBER}:=

n REPO [ @XREF:REPO@ | <NULL>] {1:1} p.27
    +1 CALN <SOURCE_CALL_NUMBER> {0:M} p.61
        +2 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62

The number used by a repository to identify the specific items in its collections.

*/
#endregion