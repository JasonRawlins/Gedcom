using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class HeaderDto(Header header) : GedcomDto
{
    public CharacterSetDto? CharacterSet { get; set; } = GetRecord(new CharacterSetDto(header.CharacterSet));
    public string? CopyrightGedcomFile { get; set; } = GetString(header.CopyrightGedcomFile);
    public string? FileName { get; set; } = GetString(header.FileName);
    public HeaderGedcomDto? Gedcom { get; set; } = GetRecord(new HeaderGedcomDto(header.Gedcom));
    public NoteDto? GedcomContentDescription { get; set; } = GetRecord(new NoteDto(header.GedcomContentDescription));
    public string? LanguageOfText { get; set; } = GetString(header.LanguageOfText);
    public string? PlaceHierarchy { get; set; } = GetString(header.PlaceHierarchy);
    public string? ReceivingSystemName { get; set; } = GetString(header.ReceivingSystemName);
    public HeaderSourceDto? Source { get; set; } = GetRecord(new HeaderSourceDto(header.Source));
    public SubmissionDto? SubmissionRecord { get; set; } = GetRecord(new SubmissionDto(header.SubmissionRecord));
    public string? Submitter { get; set; } = GetString(header.Submitter);
    public GedcomDateDto? TransmissionDate { get; set; } = GetRecord(new GedcomDateDto(header.TransmissionDate));
    public override string ToString() => $"{Submitter}";
}