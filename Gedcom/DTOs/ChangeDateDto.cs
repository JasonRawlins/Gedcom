using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class ChangeDateDto(ChangeDate changeDate) : GedcomDto
{
    public GedcomDateDto? ChangeDate { get; set; } = GetRecord(new GedcomDateDto(changeDate.GedcomDate));
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(changeDate.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public override string ToString() => $"{ChangeDate}";
}