using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SubmissionRecordJsonConverter))]
public class SubmissionRecord : RecordStructureBase
{
    public SubmissionRecord() : base() { }
    public SubmissionRecord(Record record) : base(record) { }

    public string Submitter => _(Tag.SUBM);
    public string NameOfFamilyFile => _(Tag.FAMF);
    public string TempleCode => _(Tag.TEMP);
    public string GenerationsOfAncestors => _(Tag.ANCE);
    public string GenerationsOfDescendants => _(Tag.DESC);
    public string OrdinanceProcessFlag => _(Tag.ORDI);
    public string AutomatedRecordId => _(Tag.RIN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.NOTE);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.CHAN);
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {Submitter}";
}

internal class SubmissionRecordJsonConverter : JsonConverter<SubmissionRecord>
{
    public override SubmissionRecord? ReadJson(JsonReader reader, Type objectType, SubmissionRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SubmissionRecord? submissionRecord, JsonSerializer serializer)
    {
        if (submissionRecord == null) throw new ArgumentNullException(nameof(submissionRecord));

        serializer.Serialize(writer, new SubmissionRecordJson(submissionRecord));
    }
}

internal class SubmissionRecordJson : GedcomJson
{
    public SubmissionRecordJson(SubmissionRecord submissionRecord)
    {
        AutomatedRecordId = JsonString(submissionRecord.AutomatedRecordId);
        ChangeDate = JsonRecord(submissionRecord.ChangeDate);
        GenerationsOfAncestors = JsonString(submissionRecord.GenerationsOfAncestors);
        GenerationsOfDescendants = JsonString(submissionRecord.GenerationsOfDescendants);
        NameOfFamilyFile = JsonString(submissionRecord.NameOfFamilyFile);
        OrdinanceProcessFlag = JsonString(submissionRecord.OrdinanceProcessFlag);
        Submitter = JsonString(submissionRecord.Submitter);
        TempleCode = JsonString(submissionRecord.TempleCode);
        Xref = submissionRecord.Xref;
    }

    public string? AutomatedRecordId { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public string? GenerationsOfAncestors { get; set; }
    public string? GenerationsOfDescendants { get; set; }
    public string? NameOfFamilyFile { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public string? OrdinanceProcessFlag { get; set; }
    public string? Submitter { get; set; }
    public string? TempleCode { get; set; }
    public string? Xref { get; set; }
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