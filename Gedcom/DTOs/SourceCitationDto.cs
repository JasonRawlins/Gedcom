using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SourceCitationDto(SourceCitation sourceCitation) : GedcomDto
{
    public string? CertaintyAssessment { get; set; } = GetString(sourceCitation.CertaintyAssessment);
    public SourceCitationDataDto? Data { get; set; } = GetRecord(new SourceCitationDataDto(sourceCitation.SourceCitationData));
    public EventTypeCitedFromDto? EventTypeCitedFrom { get; set; } = GetRecord(new EventTypeCitedFromDto(sourceCitation.EventTypeCitedFrom));
    public List<MultimediaLinkDto>? MultimediaLinks { get; set; } = GetList(sourceCitation.MultimediaLinks.Select(ml => new MultimediaLinkDto(ml)).ToList());
    public List<string>? Notes { get; set; } = GetList(sourceCitation.NoteStructures.Select(ns => ns.Text).ToList());
    public string? WhereWithinSource { get; set; } = GetString(sourceCitation.WhereWithinSource);
    public string? Xref { get; set; } = sourceCitation.Xref;
    public override string ToString() => $"{WhereWithinSource}";
}