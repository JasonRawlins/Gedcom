using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderCorporationJsonConverter))]
public class HeaderCorporation : RecordStructureBase, IAddressStructure
{
    public HeaderCorporation() : base() { }
    public HeaderCorporation(Record record) : base(record) { }

    private List<string>? _addressEmails = null;
    public List<string> AddressEmails => _addressEmails ??= ListValues(Tag.Email);

    private List<string>? _addressFaxNumbers = null;
    public List<string> AddressFaxNumbers => _addressFaxNumbers ??= ListValues(Tag.Facimilie);

    private AddressStructure? _addressStructure = null;
    public AddressStructure AddressStructure => _addressStructure ??= First<AddressStructure>(Tag.Address);

    private List<string>? _addressWebPages = null;
    public List<string> AddressWebPages => _addressWebPages ??= ListValues(Tag.Web);

    private List<string>? _phoneNumbers = null;
    public List<string> PhoneNumbers => _phoneNumbers ??= ListValues(Tag.Phone);

    public override string ToString() => $"{Record.Value}, {AddressStructure.AddressLine}";
}

internal sealed class HeaderCorporationJsonConverter : JsonConverter<HeaderCorporation>
{
    public override HeaderCorporation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, HeaderCorporation value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new HeaderCorporationDto(value), GedcomDto.SerializationOptions);
    }
}

public class HeaderCorporationDto(HeaderCorporation headerCorporation) : GedcomDto
{
    public AddressDto? Address { get; set; } = Record(new AddressDto(headerCorporation.AddressStructure));
    public List<string>? Emails { get; set; } = List(headerCorporation.AddressEmails);
    public List<string>? FaxNumbers { get; set; } = List(headerCorporation.AddressFaxNumbers);
    public List<string>? PhoneNumbers { get; set; } = List(headerCorporation.PhoneNumbers);
    public List<string>? WebPages { get; set; } = List(headerCorporation.AddressWebPages);
    public override string ToString() => $"{Emails?.FirstOrDefault()}";
}

#region HeaderCorporation p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 CORP <NAME_OF_BUSINESS> {0:1} p.54
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31

*/
#endregion