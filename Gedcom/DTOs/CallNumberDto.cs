using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class CallNumberDto(CallNumber callNumber) : GedcomDto
{
    public string? SourceMediaType { get; set; } = GetString(callNumber.SourceMediaType);
    public override string ToString() => $"{SourceMediaType}";
}