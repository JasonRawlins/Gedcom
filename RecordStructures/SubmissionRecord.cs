using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(SubmissionRecord))]
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

internal class SubmissionRecordJson
{
    public SubmissionRecordJson(SubmissionRecord submissionRecord)
    {
        Xref = submissionRecord.Xref;
        Submitter = string.IsNullOrEmpty(submissionRecord.Submitter) ? null : submissionRecord.Submitter;
        NameOfFamilyFile = string.IsNullOrEmpty(submissionRecord.NameOfFamilyFile) ? null : submissionRecord.NameOfFamilyFile;
        TempleCode = string.IsNullOrEmpty(submissionRecord.TempleCode) ? null : submissionRecord.TempleCode;
        GenerationsOfAncestors = string.IsNullOrEmpty(submissionRecord.GenerationsOfAncestors) ? null : submissionRecord.GenerationsOfAncestors;
        GenerationsOfDescendants = string.IsNullOrEmpty(submissionRecord.GenerationsOfDescendants) ? null : submissionRecord.GenerationsOfDescendants;
        OrdinanceProcessFlag = string.IsNullOrEmpty(submissionRecord.OrdinanceProcessFlag) ? null : submissionRecord.OrdinanceProcessFlag;
        AutomatedRecordId = string.IsNullOrEmpty(submissionRecord.AutomatedRecordId) ? null : submissionRecord.AutomatedRecordId;
        Submitter = string.IsNullOrEmpty(submissionRecord.Submitter) ? null : submissionRecord.Submitter;
        Submitter = string.IsNullOrEmpty(submissionRecord.Submitter) ? null : submissionRecord.Submitter;
        NoteStructures = submissionRecord.NoteStructures.Count == 0 ? null : submissionRecord.NoteStructures;
        ChangeDate = submissionRecord.ChangeDate.IsEmpty ? null : submissionRecord.ChangeDate;
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