using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(ChangeDateJsonConverter))]
public class ChangeDate : RecordStructureBase
{
    public ChangeDate() { }
    public ChangeDate(Record record) : base(record) { }


    private GedcomDate? _gedcomDate = null;
    public GedcomDate GedcomDate => _gedcomDate ??= First<GedcomDate>(Tag.Date);

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    public override string ToString() => $"{Record.Value}, {GedcomDate.DayMonthYear}";
}

internal sealed class ChangeDateJsonConverter : JsonConverter<ChangeDate>
{
    public override ChangeDate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, ChangeDate value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new ChangeDateJson(value), options);
    }
}

public class ChangeDateJson(ChangeDate changeDate) : GedcomJson
{
    public GedcomDateJson? ChangeDate { get; set; } = JsonRecord(new GedcomDateJson(changeDate.GedcomDate));
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(changeDate.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public override string ToString() => $"{ChangeDate}";
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