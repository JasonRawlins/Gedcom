using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SubmitterDto(SubmitterRecord submitterRecord) : GedcomDto
{
    public AddressDto? Address { get; set; } = GetRecord(new AddressDto(submitterRecord.AddressStructure));
    public string? AutomatedRecordId { get; set; } = GetString(submitterRecord.AutomatedRecordId);
    public GedcomDateDto? ChangeDate { get; set; } = GetRecord(new GedcomDateDto(submitterRecord.ChangeDate));
    public List<string>? LanguagePreferences { get; set; } = GetList(submitterRecord.LanguagePreferences);
    public List<MultimediaLinkDto>? MultimediaLinks { get; set; } = GetList(submitterRecord.MultimediaLinks.Select(ml => new MultimediaLinkDto(ml)).ToList());
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(submitterRecord.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? SubmitterName { get; set; } = GetString(submitterRecord.SubmitterName);
    public string? SubmitterRegisteredReferenceNumber { get; set; } = GetString(submitterRecord.SubmitterRegisteredRfn);

    public override string ToString() => $"{SubmitterName}";
}