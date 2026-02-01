using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SubmissionJsonConverter))]
public class SubmissionRecord : RecordStructureBase
{
    public SubmissionRecord() : base() { }
    public SubmissionRecord(Record record) : base(record) { }

    private string? automatedRecordId = null;
    public string AutomatedRecordId => automatedRecordId ??= GetValue(Tag.RecordIdNumber);

    private ChangeDate? changeDate = null;
    public ChangeDate ChangeDate => changeDate ??= First<ChangeDate>(Tag.Change);

    private string? generationsOfAncestors = null;
    public string GenerationsOfAncestors => generationsOfAncestors ??= GetValue(Tag.Ancestors);

    private string? generationsOfDescendants = null;
    public string GenerationsOfDescendants => generationsOfDescendants ??= GetValue(Tag.Descendants);

    private string? nameOfFamilyFile = null;
    public string NameOfFamilyFile => nameOfFamilyFile ??= GetValue(Tag.FamilyFile);

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? ordinanceProcessFlag = null;
    public string OrdinanceProcessFlag => ordinanceProcessFlag ??= GetValue(Tag.Ordinance);

    private string? submitter = null;
    public string Submitter => submitter ??= GetValue(Tag.Submitter);

    private string? templeCode = null;
    public string TempleCode => templeCode ??= GetValue(Tag.Temple);

    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {Submitter}";
}

internal sealed class SubmissionJsonConverter : JsonConverter<SubmissionRecord>
{
    public override SubmissionRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, SubmissionRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new SubmissionJson(value), options);
    }
}

public class SubmissionJson(SubmissionRecord submissionRecord) : GedcomJson
{
    public string? AutomatedRecordId { get; set; } = JsonString(submissionRecord.AutomatedRecordId);
    public ChangeDateJson? ChangeDate { get; set; } = JsonRecord(new ChangeDateJson(submissionRecord.ChangeDate));
    public string? GenerationsOfAncestors { get; set; } = JsonString(submissionRecord.GenerationsOfAncestors);
    public string? GenerationsOfDescendants { get; set; } = JsonString(submissionRecord.GenerationsOfDescendants);
    public string? NameOfFamilyFile { get; set; } = JsonString(submissionRecord.NameOfFamilyFile);
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(submissionRecord.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? OrdinanceProcessFlag { get; set; } = JsonString(submissionRecord.OrdinanceProcessFlag);
    public string? Submitter { get; set; } = JsonString(submissionRecord.Submitter);
    public string? TempleCode { get; set; } = JsonString(submissionRecord.TempleCode);
    public string? Xref { get; set; } = submissionRecord.Xref;

    public override string ToString() => $"{Submitter}";
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