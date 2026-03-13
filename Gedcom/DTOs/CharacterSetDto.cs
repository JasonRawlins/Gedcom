using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class CharacterSetDto(CharacterSet characterSet) : GedcomDto
{
    public string? VersionNumber { get; set; } = GetString(characterSet.VersionNumber);
    public override string ToString() => $"{VersionNumber}";
}