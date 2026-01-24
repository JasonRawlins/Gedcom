using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderTreeJsonConverter))]
public class HeaderTree : RecordStructureBase
{
    public HeaderTree() : base() { }
    public HeaderTree(Record record) : base(record) { }

    public string AutomatedRecordId => GetValue(Tag.RecordIdNumber);
    public string Name => Record.Value;
    public NoteStructure Note => First<NoteStructure>(Tag.Note);

    public override string ToString() => $"{Record.Value}, {Name}";
}

internal class HeaderTreeJsonConverter : JsonConverter<HeaderTree>
{
    public override HeaderTree? ReadJson(JsonReader reader, Type objectType, HeaderTree? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, HeaderTree? headerTree, JsonSerializer serializer)
    {
        if (headerTree == null) throw new ArgumentNullException(nameof(headerTree));

        serializer.Serialize(writer, new HeaderTreeJson(headerTree));
    }
}

public class HeaderTreeJson(HeaderTree headerTree) : GedcomJson
{
    public string AutomatedRecordId { get; set; } = headerTree.AutomatedRecordId;
    public string Name { get; set; } = headerTree.Name;
    public string Note { get; set; } = headerTree.Note.Text;
}


#region HeaderTREE p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 _Tree <Ancestry_Custom_Record>
            RIN <AUTOMATED_RECORD_ID>

*/
#endregion


