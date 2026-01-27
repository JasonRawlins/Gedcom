using Gedcom.Core;
using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceRecordEventJsonConverter))]
public class SourceRecordEvent : RecordStructureBase
{
    public SourceRecordEvent() : base() { }
    public SourceRecordEvent(Record record) : base(record) { }

    public string DatePeriod => GetValue(Tag.Date);
    public string SourceJurisdictionPlace => GetValue(Tag.Place);

    public override string ToString() => $"{Record.Value}, {DatePeriod}";
}

internal class SourceRecordEventJsonConverter : JsonConverter<SourceRecordEvent>
{
    public override SourceRecordEvent? ReadJson(JsonReader reader, Type objectType, SourceRecordEvent? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SourceRecordEvent? sourceRecordEvent, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(sourceRecordEvent);
        serializer.Serialize(writer, new SourceRecordEventJson(sourceRecordEvent));
    }
}

public class SourceRecordEventJson(SourceRecordEvent sourceRecordEvent) : GedcomJson
{
    public string? DatePeriod { get; set; } = JsonString(sourceRecordEvent.DatePeriod);
    public string? SourceJurisdictionPlace { get; set; } = JsonString(sourceRecordEvent.SourceJurisdictionPlace);

    public override string ToString() => $"{SourceJurisdictionPlace} {DatePeriod}";
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