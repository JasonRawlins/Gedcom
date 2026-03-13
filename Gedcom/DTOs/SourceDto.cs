using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SourceDto : GedcomDto, IComparable<SourceDto>
{
    public SourceDto(SourceRecord sourceRecord)
    {
        AutomatedRecordId = GetString(sourceRecord.AutomatedRecordId);
        CallNumber = GetString(sourceRecord.CallNumber);
        ChangeDate = GetRecord(new ChangeDateDto(sourceRecord.ChangeDate));
        DescriptiveTitle = GetString(sourceRecord.SourceDescriptiveTitle.Text);
        FiledByEntry = GetRecord(new NoteDto(sourceRecord.SourceFiledByEntry));
        IsEmpty = sourceRecord.IsEmpty;
        MultimediaLinks = GetList(sourceRecord.MultimediaLinks.Select(ml => new MultimediaLinkDto(ml)).ToList());
        Note = GetString(sourceRecord.NoteStructures.FirstOrDefault()?.Text ?? "");
        Originator = GetString(sourceRecord.SourceOriginator.Text);
        PublicationFacts = GetString(sourceRecord.SourcePublicationFacts.Text);
        RecordData = GetRecord(new SourceDataDto(sourceRecord.SourceRecordData));
        RepositoryCitations = GetList(sourceRecord.SourceRepositoryCitations.Select(src => new SourceRepositoryCitationDto(src)).ToList());
        RepositoryXref = GetString(sourceRecord.RepositoryXref);
        TextFromSource = GetRecord(new NoteDto(sourceRecord.TextFromSource));
        UserReferenceNumbers = GetList(sourceRecord.UserReferenceNumbers.Select(urn => new UserReferenceNumberDto(urn)).ToList());
        Xref = sourceRecord.Xref;
    }

    public string? AutomatedRecordId { get; set; }
    public string? CallNumber { get; set; }
    public ChangeDateDto? ChangeDate { get; set; }
    public string? DescriptiveTitle { get; set; }
    public NoteDto? FiledByEntry { get; set; }
    public List<MultimediaLinkDto>? MultimediaLinks { get; set; }
    public string? Note { get; set; }
    public string? Originator { get; set; }
    public string? PublicationFacts { get; set; }
    public SourceDataDto? RecordData { get; set; }
    public List<SourceRepositoryCitationDto>? RepositoryCitations { get; set; }
    public string? RepositoryXref { get; set; }
    public NoteDto? TextFromSource { get; set; }
    public List<UserReferenceNumberDto>? UserReferenceNumbers { get; set; }
    public string Xref { get; set; }

    public int CompareTo(SourceDto? other)
    {
        if (other == null) return 1;

        return other.DescriptiveTitle!.CompareTo(other.DescriptiveTitle);
    }

    public override string ToString() => $"{DescriptiveTitle}";
}