using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(HeaderSourceJsonConverter))]
public class HeaderSource : RecordStructureBase
{
    public HeaderSource() : base() { }
    public HeaderSource(Record record) : base(record) { }

    public string Version => _(C.VERS);
    public string NameOfProduct => _(C.NAME);
    public HeaderCorporation Corporation => First<HeaderCorporation>(C.CORP);
    public HeaderData Data => First<HeaderData>(C.DATA);
    public HeaderTree Tree => First<HeaderTree>(C._TREE);
}

internal class HeaderSourceJsonConverter : JsonConverter<HeaderSource>
{
    public override HeaderSource? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderSource headerSource, JsonSerializerOptions options)
    {
        var headerSOURJson = new HeaderSourceJson(headerSource);
        JsonSerializer.Serialize(writer, headerSOURJson, headerSOURJson.GetType(), options);
    }
}

internal class HeaderSourceJson : GedcomJson
{
    public HeaderSourceJson(HeaderSource headerSource)
    {
        Xref = JsonString(headerSource.Xref);
        Version = JsonString(headerSource.Version);
        NameOfProduct = JsonString(headerSource.NameOfProduct);
        Corporation = JsonRecord(headerSource.Corporation);
        Data = JsonRecord(headerSource.Data);
        Tree = JsonRecord(headerSource.Tree);
    }

    public string? Version { get; set; }
    public string? NameOfProduct { get; set; }
    public HeaderCorporation? Corporation { get; set; }
    public HeaderData? Data { get; set; }
    public HeaderTree? Tree { get; set; }
}


#region HeaderSource p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 VERS <VERSION_NUMBER> {0:1} p.64
        +2 NAME <NAME_OF_PRODUCT> {0:1} p.54
        +2 CORP <NAME_OF_BUSINESS> {0:1} p.54
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31
        +2 DATA <NAME_OF_SOURCE_DATA> {0:1} p.54
        +2 _Tree <Ancestry_Custom_Record>
            +3 RIN <AUTOMATED_RECORD_ID>

*/
#endregion



