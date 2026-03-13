using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class FormDto(FormRecord formRecord) : GedcomDto
{
    public string? MediaType { get; set; } = GetString(formRecord.MediaType);
    public string? SourceType { get; set; } = GetString(formRecord.SourceType);
    public string? Type { get; set; } = GetString(formRecord.Type);
    public override string ToString() => $"{Type}";
}