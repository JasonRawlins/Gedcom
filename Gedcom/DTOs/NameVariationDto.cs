using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class NameVariationDto(NameVariation nameVariation) : GedcomDto
{
    public string FullName { get; set; } = nameVariation.FullName;
    public string Given { get; set; } = nameVariation.Given;
    public string NamePrefix = nameVariation.NamePrefix;
    public string NameSuffix { get; set; } = nameVariation.NameSuffix;
    public string Nickname { get; set; } = nameVariation.Nickname;
    public string Surname { get; set; } = nameVariation.Surname;
    public string SurnamePrefix { get; set; } = nameVariation.SurnamePrefix;
    public string Type { get; set; } = nameVariation.Type;

    public override string ToString() => $"{Type}, {FullName}";
}