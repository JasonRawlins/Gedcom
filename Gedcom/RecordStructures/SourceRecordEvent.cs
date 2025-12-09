using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceRecordEventJsonConverter))]
public class SourceRecordEvent : RecordStructureBase
{
    public SourceRecordEvent() : base() { }
    public SourceRecordEvent(Record record) : base(record) { }

    public string DatePeriod => _(Tag.DATE);
    public string SourceJurisdictionPlace => _(Tag.PLAC);

    public override string ToString() => $"{Record.Value}, {DatePeriod}";
}

internal class SourceRecordEventJsonConverter : JsonConverter<SourceRecordEvent>
{
    public override SourceRecordEvent? ReadJson(JsonReader reader, Type objectType, SourceRecordEvent? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SourceRecordEvent? sourceRecordEvent, JsonSerializer serializer)
    {
        if (sourceRecordEvent == null) throw new ArgumentNullException(nameof(sourceRecordEvent));

        serializer.Serialize(writer, new SourceRecordEventJson(sourceRecordEvent));
    }
}

internal class SourceRecordEventJson : GedcomJson
{
    public SourceRecordEventJson(SourceRecordEvent sourceRecordEvent)
    {
        DatePeriod = JsonString(sourceRecordEvent.DatePeriod);
        SourceJurisdictionPlace = JsonString(sourceRecordEvent.SourceJurisdictionPlace);
    }

    public string? DatePeriod { get; set; }
    public string? SourceJurisdictionPlace { get; set; }
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