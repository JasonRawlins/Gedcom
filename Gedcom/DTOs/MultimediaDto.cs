using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

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
