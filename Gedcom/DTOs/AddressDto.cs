using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class AddressDto(AddressStructure addressStructure) : GedcomDto
{
    public string? City { get; set; } = GetString(addressStructure.AddressCity);
    public string? Country { get; set; } = GetString(addressStructure.AddressCountry);
    public string? Line { get; set; } = GetString(addressStructure.AddressLine);
    public string? Line1 { get; set; } = GetString(addressStructure.AddressLine1);
    public string? Line2 { get; set; } = GetString(addressStructure.AddressLine2);
    public string? Line3 { get; set; } = GetString(addressStructure.AddressLine3);
    public string? PostalCode { get; set; } = GetString(addressStructure.AddressPostCode);
    public string? State { get; set; } = GetString(addressStructure.AddressState);

    public override string ToString() => $"{Line}, {City}, {State}, {PostalCode}";
}