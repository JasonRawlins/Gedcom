using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaRecordJsonConverter))]
public class MultimediaRecord : RecordStructureBase
{
    public MultimediaRecord() : base() { }
    public MultimediaRecord(Record record) : base(record) { }

    private string? _automatedRecordId = null;
    public string AutomatedRecordId => _automatedRecordId ??= GetValue(Tag.RecordIdNumber);

    private ChangeDate? _changeDate = null;
    public ChangeDate ChangeDate => _changeDate ??= First<ChangeDate>(Tag.Change);

    // The DATE value is not in the specification, but is in the gedcom exported from Ancestry.
    private string? _date = null;
    public string Date => _date ??= GetValue(Tag.Date);

    private string? _description = null;
    public string Description => _description ??= GetValue(ExtensionTag.Ancestry.Description);

    private string? _descriptiveTitle = null;
    public string DescriptiveTitle => _descriptiveTitle ??= GetValue(Tag.Title);

    private FileRecord? _fileRecord = null;
    public FileRecord FileRecord => _fileRecord ??= First<FileRecord>(Tag.File);

    private List<string>? _multimediaFileReferenceNumbers = null;
    public List<string> MultimediaFileReferenceNumbers => _multimediaFileReferenceNumbers ??= GetStringList(Tag.File);

    private MultimediaFormat? _multimediaFormat = null;
    public MultimediaFormat MultimediaFormat => _multimediaFormat ??= First<MultimediaFormat>(Tag.Format);

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? _objectId = null;
    public string ObjectId => _objectId ??= GetValue(ExtensionTag.Ancestry.ObjectId);

    // The PLAC line is not in the specification, but is in the gedcom exported from Ancestry.
    private PlaceStructure? _placeStructure = null;
    public PlaceStructure PlaceStructure => _placeStructure ??= First<PlaceStructure>(Tag.Place);

    private List<SourceCitation>? _sourceCitations = null;
    public List<SourceCitation> SourceCitations => _sourceCitations ??= List<SourceCitation>(Tag.Source);

    private UserReferenceNumber? _userReferenceNumber = null;
    public UserReferenceNumber UserReferenceNumber => _userReferenceNumber ??= First<UserReferenceNumber>(Tag.Reference);

    public override string ToString() => $"{Record.Value}, {AutomatedRecordId}, {DescriptiveTitle}";
}

internal sealed class MultimediaRecordJsonConverter : JsonConverter<MultimediaRecord>
{
    public override MultimediaRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, MultimediaRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new MultimediaDto(value), GedcomDto.SerializationOptions);
    }
}

public class MultimediaDto(MultimediaRecord multimediaRecord) : GedcomDto
{
    public string? AutomatedRecordId { get; set; } = GetString(multimediaRecord.AutomatedRecordId);
    public ChangeDateDto? ChangeDate { get; set; } = GetRecord(new ChangeDateDto(multimediaRecord.ChangeDate));
    public string? Date { get; set; } = GetString(multimediaRecord.Date);
    public string? Description { get; set; } = GetString(multimediaRecord.Description);
    public string? DescriptiveTitle { get; set; } = GetString(multimediaRecord.DescriptiveTitle);
    public FileDto? File { get; set; } = GetRecord(new FileDto(multimediaRecord.FileRecord));
    public List<string>? MultimediaFileReferenceNumbers { get; set; } = GetList(multimediaRecord.MultimediaFileReferenceNumbers);
    public MultimediaFormatDto? MultimediaFormat { get; set; } = GetRecord(new MultimediaFormatDto(multimediaRecord.MultimediaFormat));
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(multimediaRecord.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? ObjectId { get; set; } = GetString(multimediaRecord.ObjectId);
    public PlaceDto? Place { get; set; } = GetRecord(new PlaceDto(multimediaRecord.PlaceStructure));
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(multimediaRecord.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());
    public UserReferenceNumberDto? UserReferenceNumber { get; set; } = GetRecord(new UserReferenceNumberDto(multimediaRecord.UserReferenceNumber));
    public override string ToString() => $"{Description}";
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