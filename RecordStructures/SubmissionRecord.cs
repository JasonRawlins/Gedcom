using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(SubmissionRecordJsonConverter))]
public class SubmissionRecord : RecordStructureBase
{
    public SubmissionRecord() : base() { }
    public SubmissionRecord(Record record) : base(record) { }

    public string Xref => Record.Value;
    public string Submitter => _(C.SUBM);
    public string NameOfFamilyFile => _(C.FAMF);
    public string TempleCode => _(C.TEMP);
    public string GenerationsOfAncestors => _(C.ANCE);
    public string GenerationsOfDescendants => _(C.DESC);
    public string OrdinanceProcessFlag => _(C.ORDI);
    public string AutomatedRecordId => _(C.RIN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
}

internal class SubmissionRecordJsonConverter : JsonConverter<SubmissionRecord>
{
    public override SubmissionRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, SubmissionRecord submissionRecord, JsonSerializerOptions options)
    {
        var submissionRecordJson = new SubmissionRecordJson(submissionRecord);
        JsonSerializer.Serialize(writer, submissionRecordJson, submissionRecordJson.GetType(), options);
    }
}

internal class SubmissionRecordJson : GedcomJson
{
    public SubmissionRecordJson(SubmissionRecord submissionRecord)
    {
        Xref = submissionRecord.Xref;
        Submitter = JsonString(submissionRecord.Submitter);
        NameOfFamilyFile = JsonString(submissionRecord.NameOfFamilyFile);
        TempleCode = JsonString(submissionRecord.TempleCode);
        GenerationsOfAncestors = JsonString(submissionRecord.GenerationsOfAncestors);
        GenerationsOfDescendants = JsonString(submissionRecord.GenerationsOfDescendants);
        OrdinanceProcessFlag = JsonString(submissionRecord.OrdinanceProcessFlag);
        AutomatedRecordId = JsonString(submissionRecord.AutomatedRecordId);
        NoteStructures = JsonList(submissionRecord.NoteStructures);
        ChangeDate = JsonRecord(submissionRecord.ChangeDate);
    }

    public string? Xref { get; set; }
    public string? Submitter { get; set; }
    public string? NameOfFamilyFile { get; set; }
    public string? TempleCode { get; set; }
    public string? GenerationsOfAncestors { get; set; }
    public string? GenerationsOfDescendants { get; set; }
    public string? OrdinanceProcessFlag { get; set; }
    public string? AutomatedRecordId { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public ChangeDate? ChangeDate { get; set; }
}

#region SUBMISSION_RECORD p. 28
/* 

SUBMISSION_RECORD:=

n @XREF:SUBN@ SUBN {1:1}
    +1 SUBM @XREF:SUBM@ {0:1} p.28
    +1 FAMF <NAME_OF_FAMILY_FILE> {0:1} p.54
    +1 TEMP <TEMPLE_CODE> {0:1} p.63
    +1 ANCE <GENERATIONS_OF_ANCESTORS> {0:1} p.50
    +1 DESC <GENERATIONS_OF_DESCENDANTS> {0:1} p.50
    +1 ORDI <ORDINANCE_PROCESS_FLAG> {0:1} p.57
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<CHANGE_DATE>> {0:1} p.31

The sending system uses a submission record to send instructions and information to the receiving
system. TempleReady processes submission records to determine which temple the cleared records
should be directed to. The submission record is also used for communication between Ancestral File
download requests and TempleReady. Each GEDCOM transmission file should have only one
submission record. Multiple submissions are handled by creating separate GEDCOM transmission
files.

*/
#endregion