using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderSourceJsonConverter))]
public class HeaderSource : RecordStructureBase
{
    public HeaderSource() : base() { }
    public HeaderSource(Record record) : base(record) { }

    private HeaderCorporation? corporation = null;
    public HeaderCorporation Corporation => corporation ??= First<HeaderCorporation>(Tag.Corporate);

    private HeaderData? data = null;
    public HeaderData Data => data ??= First<HeaderData>(Tag.Data);

    private string? nameOfProduct = null;
    public string NameOfProduct => nameOfProduct ??= GetValue(Tag.Name);

    private HeaderTree? tree = null;
    public HeaderTree Tree => tree ??= First<HeaderTree>(ExtensionTag.Tree);

    private string? version = null;
    public string Version => version ??= GetValue(Tag.Version);
    
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {NameOfProduct}, {Version}";
}

internal sealed class HeaderSourceJsonConverter : JsonConverter<HeaderSource>
{
    public override HeaderSource? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, HeaderSource value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new HeaderSourceJson(value), options);
    }
}

public class HeaderSourceJson(HeaderSource headerSource) : GedcomJson
{
    public HeaderCorporationJson? Corporation { get; set; } = JsonRecord(new HeaderCorporationJson(headerSource.Corporation));
    public HeaderDataJson? Data { get; set; } = JsonRecord(new HeaderDataJson(headerSource.Data));
    public string? NameOfProduct { get; set; } = JsonString(headerSource.NameOfProduct);
    public HeaderTreeJson? Tree { get; set; } = JsonRecord(new HeaderTreeJson(headerSource.Tree));
    public string? Version { get; set; } = JsonString(headerSource.Version);
    public string? Xref { get; set; } = JsonString(headerSource.Xref);
    public override string ToString() => $"{Tree?.Name}";
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



