using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(ChangeDateJsonConverter))]
public class ChangeDate : RecordStructureBase
{
    public ChangeDate() { }
    public ChangeDate(Record record) : base(record) { }

    public GedcomDate GedcomDate => First<GedcomDate>(Tag.Date);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);

    public override string ToString() => $"{Record.Value}, {GedcomDate.DayMonthYear}";
}

internal class ChangeDateJsonConverter : JsonConverter<ChangeDate>
{
    public override ChangeDate? ReadJson(JsonReader reader, Type objectType, ChangeDate? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, ChangeDate? changeDate, JsonSerializer serializer)
    {
        if (changeDate == null) throw new ArgumentNullException(nameof(changeDate));

        serializer.Serialize(writer, new ChangeDateJson(changeDate));
    }
}

public class ChangeDateJson(ChangeDate changeDate) : GedcomJson
{
    public GedcomDateJson? ChangeDate { get; set; } = JsonRecord(new GedcomDateJson(changeDate.GedcomDate));
    public List<NoteJson>? Notes { get; set; } = JsonList(changeDate.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
}

#region CHANGE_DATE (CHAN) p. 31
/* 

CHANGE_DATE:=

n CHAN {1:1}
    +1 DATE <CHANGE_DATE> {1:1} p.44
        +2 TIME <TIME_VALUE> {0:1} p.63
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    
    The change date is intended to only record the last change to a record. Some systems may want to
    manage the change process with more detail, but it is sufficient for GEDCOM purposes to indicate
    the last time that a record was modified.

 */
#endregion