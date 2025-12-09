using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(NoteRecordJsonConverter))]
public class NoteRecord : RecordStructureBase
{
    public NoteRecord() : base() { }
    public NoteRecord(Record record) : base(record) { }

    public string AutomatedRecordId => _(Tag.RIN);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.CHAN);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(Tag.REFN);

    public override string ToString() => $"{Record.Value}, {AutomatedRecordId}";
}

internal class NoteRecordJsonConverter : JsonConverter<NoteRecord>
{
    public override NoteRecord? ReadJson(JsonReader reader, Type objectType, NoteRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, NoteRecord? noteRecord, JsonSerializer serializer)
    {
        if (noteRecord == null) throw new ArgumentNullException(nameof(noteRecord));

        serializer.Serialize(writer, new NoteRecordJson(noteRecord));
    }
}

internal class NoteRecordJson : GedcomJson
{
    public NoteRecordJson(NoteRecord noteRecord)
    {
        AutomatedRecordId = JsonString(noteRecord.AutomatedRecordId);
        ChangeDate = JsonRecord(noteRecord.ChangeDate);
        UserReferenceNumber = JsonRecord(noteRecord.UserReferenceNumber);
    }

    public string? AutomatedRecordId { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public UserReferenceNumber? UserReferenceNumber { get; set; }
}

#region NOTE_RECORD (NOTE) p. 27
/* 

NOTE_RECORD:=

n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT> {1:1} p.63
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<CHANGE_DATE>> {0:1} p.31

*/
#endregion