using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class HeaderTreeDto(HeaderTree headerTree) : GedcomDto
{
    public string AutomatedRecordId { get; set; } = headerTree.AutomatedRecordId;
    public string Name { get; set; } = headerTree.Name;
    public string Note { get; set; } = headerTree.Note.Text;

    public override string ToString() => $"{Name}";
}