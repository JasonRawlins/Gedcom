using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaRecordJsonConverter))]
public class MultimediaRecord : RecordStructureBase
{
    public MultimediaRecord() : base() { }
    public MultimediaRecord(Record record) : base(record) { }

    public string AutomatedRecordId => _(Tag.RIN);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.CHAN);
    public string DescriptiveTitle => _(Tag.TITL);
    public List<string> MultimediaFileReferenceNumbers => List(r => r.Tag.Equals(Tag.FILE)).Select(r => r.Value).ToList();
    public MultimediaFormat MultimediaFormat => First<MultimediaFormat>(Tag.FORM);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.NOTE);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.SOUR);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(Tag.REFN);

    public override string ToString() => $"{Record.Value}, {AutomatedRecordId}, {DescriptiveTitle}";
}

internal class MultimediaRecordJsonConverter : JsonConverter<MultimediaRecord>
{
    public override MultimediaRecord? ReadJson(JsonReader reader, Type objectType, MultimediaRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, MultimediaRecord? multimediaRecord, JsonSerializer serializer)
    {
        if (multimediaRecord == null) throw new ArgumentNullException(nameof(multimediaRecord));

        serializer.Serialize(writer, new MultimediaRecordJson(multimediaRecord));
    }
}

internal class MultimediaRecordJson : GedcomJson
{
    public MultimediaRecordJson(MultimediaRecord multimediaRecord)
    {
        AutomatedRecordId = JsonString(multimediaRecord.AutomatedRecordId);
        ChangeDate = JsonRecord(multimediaRecord.ChangeDate);
        DescriptiveTitle = JsonString(multimediaRecord.DescriptiveTitle);
        MultimediaFileReferenceNumbers = JsonList(multimediaRecord.MultimediaFileReferenceNumbers);
        MultimediaFormat = JsonRecord(multimediaRecord.MultimediaFormat);
        Notes = JsonList(multimediaRecord.NoteStructures);
        SourceCitations = JsonList(multimediaRecord.SourceCitations);
        UserReferenceNumber = JsonRecord(multimediaRecord.UserReferenceNumber);
    }

    public string? AutomatedRecordId { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public string? DescriptiveTitle { get; set; }
    public List<string>? MultimediaFileReferenceNumbers { get; set; }
    public MultimediaFormat? MultimediaFormat { get; set; }
    public List<NoteStructure>? Notes { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public UserReferenceNumber? UserReferenceNumber { get; set; }
}

#region MULTIMEDIA_RECORD p. 26
/* 

n @XREF:OBJE@ OBJE {1:1}
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 TYPE <SOURCE_MEDIA_TYPE> {0:1} p.62
        +2 TITL <DESCRIPTIVE_TITLE> {0:1} p.48
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<CHANGE_DATE>> {0:1} p.31

The BLOB context of the multimedia record was removed in version 5.5.1. A reference to a multimedia
file was added to the record structure. The file reference occurs one to many times so that multiple files
can be grouped together, each pertaining to the same context. For example, if you wanted to associate a
sound clip and a photo, you would reference each multimedia file and indicate the format using the
FORM tag subordinate to each file reference.

*/
#endregion