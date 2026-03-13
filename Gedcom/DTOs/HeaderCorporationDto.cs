using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class HeaderCorporationDto(HeaderCorporation headerCorporation) : GedcomDto
{
    public AddressDto? Address { get; set; } = GetRecord(new AddressDto(headerCorporation.AddressStructure));
    public List<string>? Emails { get; set; } = GetList(headerCorporation.AddressEmails);
    public List<string>? FaxNumbers { get; set; } = GetList(headerCorporation.AddressFaxNumbers);
    public List<string>? PhoneNumbers { get; set; } = GetList(headerCorporation.PhoneNumbers);
    public List<string>? WebPages { get; set; } = GetList(headerCorporation.AddressWebPages);
    public override string ToString() => $"{Emails?.FirstOrDefault()}";
}