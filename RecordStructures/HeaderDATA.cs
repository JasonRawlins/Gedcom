using System.Text.Json.Serialization;
using System.Text.Json;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(HeaderDATAJsonConverter))]
public class HeaderDATA : RecordStructureBase
{
    public HeaderDATA() : base() { }
    public HeaderDATA(Record record) : base(record) { }

    public string PublicationDate => _(C.DATE);
    public NoteStructure CopyrightSourceData => First<NoteStructure>(C.COPR);
}

internal class HeaderDATAJsonConverter : JsonConverter<HeaderDATA>
{
    public override HeaderDATA? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderDATA headerDATA, JsonSerializerOptions options)
    {
        var headerDATAJson = new HeaderDATAJson(headerDATA);
        JsonSerializer.Serialize(writer, headerDATAJson, headerDATAJson.GetType(), options);
    }
}

internal class HeaderDATAJson : GedcomJson
{
    public HeaderDATAJson(HeaderDATA headerDATA)
    {
        PublicationDate = JsonString(headerDATA.PublicationDate);
        CopyrightSourceData = JsonRecord(headerDATA.CopyrightSourceData);
    }

    public string? PublicationDate { get; set; }
    public NoteStructure? CopyrightSourceData { get; set; }
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