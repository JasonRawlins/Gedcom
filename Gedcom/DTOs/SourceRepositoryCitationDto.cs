using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SourceRepositoryCitationDto(SourceRepositoryCitation sourceRepositoryCitation) : GedcomDto
{
    public List<CallNumberDto>? CallNumbers { get; set; } = GetList(sourceRepositoryCitation.CallNumbers.Select(cn => new CallNumberDto(cn)).ToList());
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(sourceRepositoryCitation.NoteStructures.Select(ns => new NoteDto(ns)).ToList());

    public override string ToString() => string.Join(", ", CallNumbers ?? []);
}