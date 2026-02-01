using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(MultimediaRecordJsonConverter))]
public class MultimediaRecord : RecordStructureBase
{
    public MultimediaRecord() : base() { }
    public MultimediaRecord(Record record) : base(record) { }

    public string AutomatedRecordId => GetValue(Tag.RecordIdNumber);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.Change);
    // The DATE value is not in the specification, but is in the gedcom exported from Ancestry.
    public string Date => GetValue(Tag.Date);
    public string Description => GetValue(ExtensionTag.Description);
    public string DescriptiveTitle => GetValue(Tag.Title);
    public FileRecord FileRecord => First<FileRecord>(Tag.File);
    public List<string> MultimediaFileReferenceNumbers => [.. List(r => r.Tag.Equals(Tag.File)).Select(r => r.Value)];
    public MultimediaFormat MultimediaFormat => First<MultimediaFormat>(Tag.Format);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string ObjectId => GetValue(ExtensionTag.ObjectId);
    // The PLAC line is not in the specification, but is in the gedcom exported from Ancestry.
    public PlaceStructure PlaceStructure => First<PlaceStructure>(Tag.Place);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(Tag.Reference);

    public override string ToString() => $"{Record.Value}, {AutomatedRecordId}, {DescriptiveTitle}";
}

internal sealed class MultimediaRecordJsonConverter : JsonConverter<MultimediaRecord>
{
    public override MultimediaRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, MultimediaRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new MultimediaJson(value), options);
    }
}

public class MultimediaJson(MultimediaRecord multimediaRecord) : GedcomJson
{
    public string? AutomatedRecordId { get; set; } = JsonString(multimediaRecord.AutomatedRecordId);
    public ChangeDateJson? ChangeDate { get; set; } = JsonRecord(new ChangeDateJson(multimediaRecord.ChangeDate));
    public string? Date { get; set; } = JsonString(multimediaRecord.Date);
    public string? Description { get; set; } = JsonString(multimediaRecord.Description);
    public string? DescriptiveTitle { get; set; } = JsonString(multimediaRecord.DescriptiveTitle);
    public FileJson? File { get; set; } = JsonRecord(new FileJson(multimediaRecord.FileRecord));
    public List<string>? MultimediaFileReferenceNumbers { get; set; } = JsonList(multimediaRecord.MultimediaFileReferenceNumbers);
    public MultimediaFormatJson? MultimediaFormat { get; set; } = JsonRecord(new MultimediaFormatJson(multimediaRecord.MultimediaFormat));
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(multimediaRecord.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? ObjectId { get; set; } = JsonString(multimediaRecord.ObjectId);
    public PlaceJson? Place { get; set; } = JsonRecord(new PlaceJson(multimediaRecord.PlaceStructure));
    public List<SourceCitationJson>? SourceCitations { get; set; } = JsonList(multimediaRecord.SourceCitations.Select(sc => new SourceCitationJson(sc)).ToList());
    public UserReferenceNumberJson? UserReferenceNumber { get; set; } = JsonRecord(new UserReferenceNumberJson(multimediaRecord.UserReferenceNumber));
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