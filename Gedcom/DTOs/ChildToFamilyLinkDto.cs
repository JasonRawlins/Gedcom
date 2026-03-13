using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class ChildToFamilyLinkDto(ChildToFamilyLink childToFamilyLink) : GedcomDto
{
    public string? AdoptedByWhichParent { get; set; } = GetString(childToFamilyLink.AdoptedByWhichParent);
    public string? ChildLinkageStatus { get; set; } = GetString(childToFamilyLink.ChildLinkageStatus);
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(childToFamilyLink.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? PedigreeLinkageType { get; set; } = GetString(childToFamilyLink.PedigreeLinkageType);
    public string? Xref { get; set; } = GetString(childToFamilyLink.Xref);
    public override string ToString() => $"{Xref}";
}