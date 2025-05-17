using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderSourceJsonConverter))]
public class HeaderSource : RecordStructureBase
{
    public HeaderSource() : base() { }
    public HeaderSource(Record record) : base(record) { }

    public HeaderCorporation Corporation => First<HeaderCorporation>(C.CORP);
    public HeaderData Data => First<HeaderData>(C.DATA);
    public string NameOfProduct => _(C.NAME);
    public HeaderTree Tree => First<HeaderTree>(C._TREE);
    public string Version => _(C.VERS);
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {NameOfProduct}, {Version}";
}

internal class HeaderSourceJsonConverter : JsonConverter<HeaderSource>
{
    public override HeaderSource? ReadJson(JsonReader reader, Type objectType, HeaderSource? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, HeaderSource? headerSource, JsonSerializer serializer)
    {
        if (headerSource == null) throw new ArgumentNullException(nameof(headerSource));

        serializer.Serialize(writer, new HeaderSourceJson(headerSource));
    }
}

internal class HeaderSourceJson : GedcomJson
{
    public HeaderSourceJson(HeaderSource headerSource)
    {
        Corporation = JsonRecord(headerSource.Corporation);
        Data = JsonRecord(headerSource.Data);
        NameOfProduct = JsonString(headerSource.NameOfProduct);
        Tree = JsonRecord(headerSource.Tree);
        Version = JsonString(headerSource.Version);
        Xref = JsonString(headerSource.Xref);
    }

    public HeaderCorporation? Corporation { get; set; }
    public HeaderData? Data { get; set; }
    public string? NameOfProduct { get; set; }
    public HeaderTree? Tree { get; set; }
    public string? Version { get; set; }
    public string? Xref { get; set; }
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



