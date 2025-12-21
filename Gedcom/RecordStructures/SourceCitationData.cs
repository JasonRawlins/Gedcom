using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceCitationDataJsonConverter))]
public class SourceCitationData : RecordStructureBase
{
    public SourceCitationData() : base() { }
    public SourceCitationData(Record record) : base(record) { }

    public string EntryRecordingDate => GetValue(Tag.Date);
    public List<NoteStructure> TextFromSources => List<NoteStructure>(Tag.Text);

    public override string ToString() => $"{Record.Value}, {EntryRecordingDate}";
}

internal class SourceCitationDataJsonConverter : JsonConverter<SourceCitationData>
{
    public override SourceCitationData? ReadJson(JsonReader reader, Type objectType, SourceCitationData? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SourceCitationData? sourceCitationData, JsonSerializer serializer)
    {
        if (sourceCitationData == null) throw new ArgumentNullException(nameof(sourceCitationData));

        serializer.Serialize(writer, new SourceCitationDataJson(sourceCitationData));
    }
}

internal class SourceCitationDataJson : GedcomJson
{
    public SourceCitationDataJson(SourceCitationData sourceCitationData)
    {
        EntryRecordingDate = JsonString(sourceCitationData.EntryRecordingDate);        
        TextFromSources = sourceCitationData.TextFromSources.Select(t => t.Text).ToList();
    }

    public string? EntryRecordingDate { get; set; }
    public List<string> TextFromSources { get; set; }
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