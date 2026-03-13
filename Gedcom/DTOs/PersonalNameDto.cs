using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class PersonalNameDto(PersonalNameStructure personalNameStructure) : GedcomDto
{
    public string? Given { get; set; } = GetString(personalNameStructure.Given);
    public string? Name { get; set; } = GetString(personalNameStructure.NamePersonal);
    public string? Nickname { get; set; } = GetString(personalNameStructure.Nickname);
    public NameVariationDto? PhoneticVariation { get; set; } = GetRecord(new NameVariationDto(personalNameStructure.NamePhoneticVariation));
    public string? Prefix { get; set; } = GetString(personalNameStructure.NamePrefix);
    public NameVariationDto? RomanizedVariation { get; set; } = GetRecord(new NameVariationDto(personalNameStructure.NameRomanizedVariation));
    public string? Suffix { get; set; } = GetString(personalNameStructure.NameSuffix);
    public string? Surname { get; set; } = GetString(personalNameStructure.Surname);
    public string? SurnamePrefix { get; set; } = GetString(personalNameStructure.SurnamePrefix);
    public string? Type { get; set; } = GetString(personalNameStructure.NameType);
    public override string ToString() => $"{Given} {Surname}";
}