using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SpouseToFamilyLinkDto(SpouseToFamilyLink spouseToFamilyLink) : GedcomDto
{
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(spouseToFamilyLink.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string Xref { get; set; } = spouseToFamilyLink.Xref;

    public override string ToString() => Xref;
}