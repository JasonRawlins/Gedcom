using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class AssociationDto(AssociationStructure associationStructure) : GedcomDto
{
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(associationStructure.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? RelationIsDescriptor { get; set; } = GetString(associationStructure.RelationIsDescriptor);
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(associationStructure.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());
    public override string ToString() => $"{RelationIsDescriptor}";
}