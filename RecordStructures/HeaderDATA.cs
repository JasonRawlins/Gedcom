using System.Text.Json.Serialization;
using System.Text.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderDataJsonConverter))]
public class HeaderData : RecordStructureBase
{
    public HeaderData() : base() { }
    public HeaderData(Record record) : base(record) { }

    public NoteStructure CopyrightSourceData => First<NoteStructure>(C.COPR);
    public string PublicationDate => _(C.DATE);

    public override string ToString() => $"{Record.Value}, {PublicationDate}";
}

internal class HeaderDataJsonConverter : JsonConverter<HeaderData>
{
    public override HeaderData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderData headerData, JsonSerializerOptions options)
    {
        var headerDATAJson = new HeaderDataJson(headerData);
        JsonSerializer.Serialize(writer, headerDATAJson, headerDATAJson.GetType(), options);
    }
}

internal class HeaderDataJson : GedcomJson
{
    public HeaderDataJson(HeaderData headerData)
    {
        CopyrightSourceData = JsonRecord(headerData.CopyrightSourceData);
        PublicationDate = JsonString(headerData.PublicationDate);
    }

    public NoteStructure? CopyrightSourceData { get; set; }
    public string? PublicationDate { get; set; }
}

#region HeaderSOUR p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 DATA <NAME_OF_SOURCE_DATA> {0:1} p.54
            +3 DATE <PUBLICATION_DATE> {0:1) p.59
            +3 COPR <COPYRIGHT_SOURCE_DATA> {0:1) p.44
                +4 [CONT|CONC]<COPYRIGHT_SOURCE_DATA> {0

*/
#endregion