using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderDataJsonConverter))]
public class HeaderData : RecordStructureBase
{
    public HeaderData() : base() { }
    public HeaderData(Record record) : base(record) { }

    private NoteStructure? copyrightSourceData = null;
    public NoteStructure CopyrightSourceData => copyrightSourceData ??= First<NoteStructure>(Tag.Copyright);

    private string? publicationDate = null;
    public string PublicationDate => publicationDate ??= GetValue(Tag.Date);

    public override string ToString() => $"{Record.Value}, {PublicationDate}";
}

internal sealed class HeaderDataJsonConverter : JsonConverter<HeaderData>
{
    public override HeaderData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, HeaderData value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new HeaderDataJson(value), options);
    }
}

public class HeaderDataJson(HeaderData headerData) : GedcomJson
{
    public NoteJson? CopyrightSourceData { get; set; } = JsonRecord(new NoteJson(headerData.CopyrightSourceData));
    public string? PublicationDate { get; set; } = JsonString(headerData.PublicationDate);
    public override string ToString() => $"{PublicationDate}";
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