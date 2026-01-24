using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderDataJsonConverter))]
public class HeaderData : RecordStructureBase
{
    public HeaderData() : base() { }
    public HeaderData(Record record) : base(record) { }

    public NoteStructure CopyrightSourceData => First<NoteStructure>(Tag.Copyright);
    public string PublicationDate => GetValue(Tag.Date);

    public override string ToString() => $"{Record.Value}, {PublicationDate}";
}

internal class HeaderDataJsonConverter : JsonConverter<HeaderData>
{
    public override HeaderData? ReadJson(JsonReader reader, Type objectType, HeaderData? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, HeaderData? headerData, JsonSerializer serializer)
    {
        if (headerData == null) throw new ArgumentNullException(nameof(headerData));

        serializer.Serialize(writer, new HeaderDataJson(headerData));
    }
}

public class HeaderDataJson(HeaderData headerData) : GedcomJson
{
    public NoteJson? CopyrightSourceData { get; set; } = JsonRecord(new NoteJson(headerData.CopyrightSourceData));
    public string? PublicationDate { get; set; } = JsonString(headerData.PublicationDate);
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