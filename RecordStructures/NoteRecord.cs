using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(NoteRecordJsonConverter))]
public class NoteRecord : RecordStructureBase
{
    public NoteRecord() : base() { }
    public NoteRecord(Record record) : base(record) { }

    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
}

internal class NoteRecordJsonConverter : JsonConverter<NoteRecord>
{
    public override NoteRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, NoteRecord noteRecord, JsonSerializerOptions options)
    {
        var noteRecordJson = new NoteRecordJson(noteRecord);
        JsonSerializer.Serialize(writer, noteRecordJson, noteRecordJson.GetType(), options);
    }
}

internal class NoteRecordJson : GedcomJson
{
    public NoteRecordJson(NoteRecord noteRecord)
    {
        UserReferenceNumber = JsonRecord(noteRecord.UserReferenceNumber);
        AutomatedRecordId = JsonString(noteRecord.AutomatedRecordId);
        ChangeDate = JsonRecord(noteRecord.ChangeDate);
    }

    public UserReferenceNumber? UserReferenceNumber { get; set; }
    public string? AutomatedRecordId { get; set; }
    public ChangeDate? ChangeDate { get; set; }
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