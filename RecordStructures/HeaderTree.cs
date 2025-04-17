using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(HeaderTreeJsonConverter))]
public class HeaderTree : RecordStructureBase
{
    public HeaderTree() : base() { }
    public HeaderTree(Record record) : base(record) { }

    public string AutomatedRecordId => _(C.RIN);
    public string Name => Record.Value;
}

internal class HeaderTreeJsonConverter : JsonConverter<HeaderTree>
{
    public override HeaderTree? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderTree headerTree, JsonSerializerOptions options)
    {
        var headerTreeJson = new HeaderTreeJson(headerTree);
        JsonSerializer.Serialize(writer, headerTreeJson, headerTreeJson.GetType(), options);
    }
}

internal class HeaderTreeJson : GedcomJson
{
    public HeaderTreeJson(HeaderTree headerTree)
    {
        AutomatedRecordId = headerTree.AutomatedRecordId;
        Name = headerTree.Name;
    }

    public string AutomatedRecordId { get; set; }
    public string Name { get; set; }
}


#region HeaderTREE p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 _Tree <Ancestry_Custom_Record>
            RIN <AUTOMATED_RECORD_ID>

*/
#endregion


