using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class LdsOrdinanceStatusDto(LdsOrdinanceStatus ldsOrdinanceStatus) : GedcomDto
{
    public string? ChangeDate { get; set; } = GetString(ldsOrdinanceStatus.ChangeDate);
    public string? Status { get; set; } = GetString(ldsOrdinanceStatus.Status);
    public override string ToString() => $"{Status}";
}