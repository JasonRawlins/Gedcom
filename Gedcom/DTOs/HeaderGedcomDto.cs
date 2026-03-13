using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class HeaderGedcomDto(HeaderGedcom gedc) : GedcomDto
{
    public string? GedcomForm { get; set; } = GetString(gedc.GedcomForm);
    public string? VersionNumber { get; set; } = GetString(gedc.VersionNumber);
    public override string ToString() => $"{VersionNumber}";
}