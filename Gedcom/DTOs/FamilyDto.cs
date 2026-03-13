using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class FamilyDto(FamilyRecord familyRecord) : GedcomDto
{
    public string? AdoptedByWhichParent { get; set; } = GetString(familyRecord.AdoptedByWhichParent);
    public string? AutomatedRecordNumber { get; set; } = GetString(familyRecord.AutomatedRecordNumber);
    public ChangeDateDto? ChangeDate { get; set; } = GetRecord(new ChangeDateDto(familyRecord.ChangeDate));
    public List<string>? Children { get; set; } = GetList(familyRecord.Children);
    public string? CountOfChildren { get; set; } = GetString(familyRecord.CountOfChildren);
    public EventDto? Divorce { get; set; } = GetRecord(new EventDto(familyRecord.Divorce));
    public List<EventDto>? Events { get; set; } = GetList(familyRecord.FamilyEventStructures.Select(fes => new EventDto(fes)).ToList());
    public string? Husband { get; set; } = GetString(familyRecord.Husband);
    // +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
    public EventDto? Marriage { get; set; } = GetRecord(new EventDto(familyRecord.Marriage));
    public List<MultimediaLinkDto>? MultimediaLinks { get; set; } = GetList(familyRecord.MultimediaLinks.Select(ml => new MultimediaLinkDto(ml)).ToList());
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(familyRecord.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? RestrictionNotice { get; set; } = GetString(familyRecord.RestrictionNotice);
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(familyRecord.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());
    public string? Submitter { get; set; } = GetString(familyRecord.Submitter);
    public List<UserReferenceNumberDto>? UserReferenceNumbers { get; set; } = GetList(familyRecord.UserReferenceNumbers.Select(urn => new UserReferenceNumberDto(urn)).ToList());
    public string? Wife { get; set; } = GetString(familyRecord.Wife);
    public string Xref { get; set; } = familyRecord.Xref;
    public override string ToString()
    {
        var childrenCountText = Children?.Count == 1 ? "child" : "children";

        return $"({Xref}) {Husband} and {Wife} with {Children?.Count} {childrenCountText}";
    }
}