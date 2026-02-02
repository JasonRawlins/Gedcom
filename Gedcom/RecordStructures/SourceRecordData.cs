using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceRecordDataJsonConverter))]
public class SourceRecordData : RecordStructureBase
{
    public SourceRecordData() : base() { }
    public SourceRecordData(Record record) : base(record) { }

    private List<SourceRecordEvent>? recordEvents = null;
    public List<SourceRecordEvent> RecordEvents => recordEvents ??= List<SourceRecordEvent>(Tag.Event);

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? responsibleAgency = null;
    public string ResponsibleAgency => responsibleAgency ??= GetValue(Tag.Agency);

    public override string ToString() => $"{Record.Value}, {ResponsibleAgency}";
}

internal sealed class SourceRecordDataJsonConverter : JsonConverter<SourceRecordData>
{
    public override SourceRecordData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, SourceRecordData value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new SourceDataJson(value), options);
    }
}

public class SourceDataJson(SourceRecordData sourceRecordData) : GedcomJson
{
    public List<SourceRecordEventJson>? EventsRecorded { get; set; } = JsonList(sourceRecordData.RecordEvents.Select(re => new SourceRecordEventJson(re)).ToList());
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(sourceRecordData.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? ResponsibleAgency { get; set; } = JsonString(sourceRecordData.ResponsibleAgency);

    public override string ToString() => $"{ResponsibleAgency}";
}

#region SOURCE_RECORD (DATA) p. 27
/* 

n @<XREF:SOUR>@ SOUR {1:1}
    1 DATA {0:1}
        2 EVEN <EVENTS_RECORDED> {0:M} p.50
            3 DATE <DATE_PERIOD> {0:1} p.46
            3 PLAC <SOURCE_JURISDICTION_PLACE> {0:1} p.62
        2 AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
        2 <<NOTE_STRUCTURE>> {0:M} p.37

*/
#endregion