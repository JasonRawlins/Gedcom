using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(MultimediaRecordJsonConverter))]
public class MultimediaRecord : RecordStructureBase
{
    public MultimediaRecord() : base() { }
    public MultimediaRecord(Record record) : base(record) { }

    public List<string> MultimediaFileReferenceNumbers => List(r => r.Tag.Equals(C.FILE)).Select(r => r.Value).ToList();
    public MultimediaFormat MultimediaFormat => First<MultimediaFormat>(C.FORM);
    public string DescriptiveTitle => _(C.TITL);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
}

internal class MultimediaRecordJsonConverter : JsonConverter<MultimediaRecord>
{
    public override MultimediaRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, MultimediaRecord multimediaRecord, JsonSerializerOptions options)
    {
        var jsonObject = new MultimediaRecordJson(multimediaRecord);
        JsonSerializer.Serialize(writer, jsonObject, jsonObject.GetType(), options);
    }
}

internal class MultimediaRecordJson : GedcomJson
{
    public MultimediaRecordJson(MultimediaRecord multimediaRecord)
    {
        MultimediaFileReferenceNumbers = JsonList(multimediaRecord.MultimediaFileReferenceNumbers);
        MultimediaFormat = JsonRecord(multimediaRecord.MultimediaFormat);
        DescriptiveTitle = JsonString(multimediaRecord.DescriptiveTitle);
        UserReferenceNumber = JsonRecord(multimediaRecord.UserReferenceNumber);
        AutomatedRecordId = JsonString(multimediaRecord.AutomatedRecordId);
        NoteStructures = JsonList(multimediaRecord.NoteStructures);
        SourceCitations = JsonList(multimediaRecord.SourceCitations);
        ChangeDate = JsonRecord(multimediaRecord.ChangeDate);
    }

    public List<string>? MultimediaFileReferenceNumbers { get; set; }
    public MultimediaFormat? MultimediaFormat { get; set; }
    public string? DescriptiveTitle { get; set; }
    public UserReferenceNumber? UserReferenceNumber { get; set; }
    public string? AutomatedRecordId { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public ChangeDate? ChangeDate { get; set; }
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