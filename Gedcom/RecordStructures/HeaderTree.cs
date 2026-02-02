using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderTreeJsonConverter))]
public class HeaderTree : RecordStructureBase
{
    public HeaderTree() : base() { }
    public HeaderTree(Record record) : base(record) { }

    private string? automatedRecordId = null;
    public string AutomatedRecordId => automatedRecordId ??= GetValue(Tag.RecordIdNumber);
    
    public string Name => Record.Value;

    private NoteStructure? note = null;
    public NoteStructure Note => note ??= First<NoteStructure>(Tag.Note);

    public override string ToString() => $"{Record.Value}, {Name}";
}

internal sealed class HeaderTreeJsonConverter : JsonConverter<HeaderTree>
{
    public override HeaderTree? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, HeaderTree value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new HeaderTreeJson(value), options);
    }
}

public class HeaderTreeJson(HeaderTree headerTree) : GedcomJson
{
    public string AutomatedRecordId { get; set; } = headerTree.AutomatedRecordId;
    public string Name { get; set; } = headerTree.Name;
    public string Note { get; set; } = headerTree.Note.Text;

    public override string ToString() => $"{Name}";
}


#region HeaderTREE p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 _Tree <Ancestry_Custom_Record>
            RIN <AUTOMATED_RECORD_ID>

*/
#endregion


