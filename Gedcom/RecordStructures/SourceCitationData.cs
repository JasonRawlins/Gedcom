using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceCitationDataJsonConverter))]
public class SourceCitationData : RecordStructureBase
{
    public SourceCitationData() : base() { }
    public SourceCitationData(Record record) : base(record) { }

    private string? entryRecordingDate = null;
    public string EntryRecordingDate => entryRecordingDate ??= GetValue(Tag.Date);

    private List<NoteStructure>? textFromSources = null;
    public List<NoteStructure> TextFromSources => textFromSources ??= List<NoteStructure>(Tag.Text);

    public override string ToString() => $"{Record.Value}, {EntryRecordingDate}";
}

internal sealed class SourceCitationDataJsonConverter : JsonConverter<SourceCitationData>
{
    public override SourceCitationData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, SourceCitationData value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new SourceCitationDataJson(value), options);
    }
}

public class SourceCitationDataJson(SourceCitationData sourceCitationData) : GedcomJson
{
    public string? EntryRecordingDate { get; set; } = JsonString(sourceCitationData.EntryRecordingDate);
    public List<string> TextFromSources { get; set; } = [.. sourceCitationData.TextFromSources.Select(t => t.Text)];

    public override string ToString() => $"Count: {TextFromSources.Count}";
}

#region SOUR.DATA p. 39
/* 

n SOUR @<XREF:SOUR>@ {1:1} p.27
    +1 DATA {0:1}
        +2 DATE <ENTRY_RECORDING_DATE> {0:1} p.48
        +2 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
            +3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}

*/
#endregion