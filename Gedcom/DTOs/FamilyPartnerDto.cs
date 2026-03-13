using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class FamilyPartnerDto(FamilyPartner familyPartner) : GedcomDto
{
    public string? AgeAtEvent { get; set; } = GetString(familyPartner.AgeAtEvent);
    public string? Name { get; set; } = GetString(familyPartner.Name);
    public override string ToString() => $"{Name}";
}