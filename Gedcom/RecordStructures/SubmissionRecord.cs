using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SubmissionJsonConverter))]
public class SubmissionRecord : RecordStructureBase
{
    public SubmissionRecord() : base() { }
    public SubmissionRecord(Record record) : base(record) { }

    public string AutomatedRecordId => GetValue(Tag.RecordIdNumber);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.Change);
    public string GenerationsOfAncestors => GetValue(Tag.Ancestors);
    public string GenerationsOfDescendants => GetValue(Tag.Descendants);
    public string NameOfFamilyFile => GetValue(Tag.FamilyFile);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string OrdinanceProcessFlag => GetValue(Tag.Ordinance);
    public string Submitter => GetValue(Tag.Submitter);
    public string TempleCode => GetValue(Tag.Temple);
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {Submitter}";
}

internal class SubmissionJsonConverter : JsonConverter<SubmissionRecord>
{
    public override SubmissionRecord? ReadJson(JsonReader reader, Type objectType, SubmissionRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SubmissionRecord? submissionRecord, JsonSerializer serializer)
    {
        if (submissionRecord == null) throw new ArgumentNullException(nameof(submissionRecord));

        serializer.Serialize(writer, new SubmissionJson(submissionRecord));
    }
}

public class SubmissionJson : GedcomJson
{
    public SubmissionJson(SubmissionRecord submissionRecord)
    {
        AutomatedRecordId = JsonString(submissionRecord.AutomatedRecordId);
        ChangeDate = JsonRecord(new ChangeDateJson(submissionRecord.ChangeDate));
        GenerationsOfAncestors = JsonString(submissionRecord.GenerationsOfAncestors);
        GenerationsOfDescendants = JsonString(submissionRecord.GenerationsOfDescendants);
        NameOfFamilyFile = JsonString(submissionRecord.NameOfFamilyFile);
        Notes = JsonList(submissionRecord.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
        OrdinanceProcessFlag = JsonString(submissionRecord.OrdinanceProcessFlag);
        Submitter = JsonString(submissionRecord.Submitter);
        TempleCode = JsonString(submissionRecord.TempleCode);
        Xref = submissionRecord.Xref;
    }

    public string? AutomatedRecordId { get; set; }
    public ChangeDateJson? ChangeDate { get; set; }
    public string? GenerationsOfAncestors { get; set; }
    public string? GenerationsOfDescendants { get; set; }
    public string? NameOfFamilyFile { get; set; }
    public List<NoteJson>? Notes { get; set; }
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