using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class MultimediaModel(MultimediaRecord multimediaRecord)
{
    public string? AutomatedRecordId { get; set; } = multimediaRecord.AutomatedRecordId;
    public ChangeDate? ChangeDate { get; set; } = multimediaRecord.ChangeDate;
    public string? Date { get; set; } = multimediaRecord.Date;
    public string? Description { get; set; } = multimediaRecord.Description;
    public string? DescriptiveTitle { get; set; } = multimediaRecord.DescriptiveTitle;
    public FileRecord? FileRecord { get; set; } = multimediaRecord.FileRecord;
    public List<string>? MultimediaFileReferenceNumbers { get; set; } = multimediaRecord.MultimediaFileReferenceNumbers;
    public MultimediaFormat? MultimediaFormat { get; set; } = multimediaRecord.MultimediaFormat;
    public List<NoteStructure>? Notes { get; set; } = multimediaRecord.NoteStructures;
    public string? ObjectId { get; set; } = multimediaRecord.ObjectId;
    public PlaceStructure? Place { get; set; } = multimediaRecord.PlaceStructure;
    public List<SourceCitation>? SourceCitations { get; set; } = multimediaRecord.SourceCitations;
    public UserReferenceNumber? UserReferenceNumber { get; set; } = multimediaRecord.UserReferenceNumber;

    public override string ToString() => $"{Description} ({DescriptiveTitle})";
}