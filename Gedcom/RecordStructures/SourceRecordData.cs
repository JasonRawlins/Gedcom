using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceRecordDataJsonConverter))]
public class SourceRecordData : RecordStructureBase
{
    public SourceRecordData() : base() { }
    public SourceRecordData(Record record) : base(record) { }

    public List<SourceRecordEvent> EventsRecorded => List<SourceRecordEvent>(C.EVEN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public string ResponsibleAgency => _(C.AGNC);

    public override string ToString() => $"{Record.Value}, {ResponsibleAgency}";
}

internal class SourceRecordDataJsonConverter : JsonConverter<SourceRecordData>
{
    public override SourceRecordData? ReadJson(JsonReader reader, Type objectType, SourceRecordData? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SourceRecordData? sourceRecordData, JsonSerializer serializer)
    {
        if (sourceRecordData == null) throw new ArgumentNullException(nameof(sourceRecordData));

        serializer.Serialize(writer, new SourceRecordDataJson(sourceRecordData));
    }
}

internal class SourceRecordDataJson : GedcomJson
{
    public SourceRecordDataJson(SourceRecordData sourceRecordData)
    {
        EventsRecorded = JsonList(sourceRecordData.EventsRecorded);
        Notes = JsonList(sourceRecordData.NoteStructures);
        ResponsibleAgency = JsonString(sourceRecordData.ResponsibleAgency);
    }

    public List<SourceRecordEvent>? EventsRecorded { get; set; }
    public List<NoteStructure>? Notes { get; set; }
    public string? ResponsibleAgency { get; set; }
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