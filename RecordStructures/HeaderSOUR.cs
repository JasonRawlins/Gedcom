using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(HeaderSOURJsonConverter))]
public class HeaderSOUR : RecordStructureBase
{
    public HeaderSOUR() : base() { }
    public HeaderSOUR(Record record) : base(record) { }

    public string Xref => Record.Value;
    public string Version => _(C.VERS);
    public string NameOfProduct => _(C.NAME);
    public HeaderCORP HeaderCORP => First<HeaderCORP>(C.CORP);
    public HeaderDATA HeaderDATA => First<HeaderDATA>(C.DATA);
}

internal class HeaderSOURJsonConverter : JsonConverter<HeaderSOUR>
{
    public override HeaderSOUR? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderSOUR headerSOUR, JsonSerializerOptions options)
    {
        var headerSOURJson = new HeaderSOURJson(headerSOUR);
        JsonSerializer.Serialize(writer, headerSOURJson, headerSOURJson.GetType(), options);
    }
}

internal class HeaderSOURJson : GedcomJson
{
    public HeaderSOURJson(HeaderSOUR headerSOUR)
    {
        Xref = JsonString(headerSOUR.Xref);
        Version = JsonString(headerSOUR.Version);
        NameOfProduct = JsonString(headerSOUR.NameOfProduct);
        HeaderCORP = JsonRecord(headerSOUR.HeaderCORP);
        HeaderDATA = JsonRecord(headerSOUR.HeaderDATA);
    }

    public string? Xref { get; set; }
    public string? Version { get; set; }
    public string? NameOfProduct { get; set; }
    public HeaderCORP? HeaderCORP { get; set; }
    public HeaderDATA? HeaderDATA { get; set; }
}


#region HeaderSOUR p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 VERS <VERSION_NUMBER> {0:1} p.64
        +2 NAME <NAME_OF_PRODUCT> {0:1} p.54
        +2 CORP <NAME_OF_BUSINESS> {0:1} p.54
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31
        +2 DATA <NAME_OF_SOURCE_DATA> {0:1} p.54

*/
#endregion


