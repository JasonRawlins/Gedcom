using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderCorporationJsonConverter))]
public class HeaderCorporation : RecordStructureBase, IAddressStructure
{
    public HeaderCorporation() : base() { }
    public HeaderCorporation(Record record) : base(record) { }

    public List<string> AddressEmails => ListValues(Tag.Email);
    public List<string> AddressFaxNumbers => ListValues(Tag.Facimilie);
    public AddressStructure AddressStructure => First<AddressStructure>(Tag.Address);
    public List<string> AddressWebPages => ListValues(Tag.Web);
    public List<string> PhoneNumbers => ListValues(Tag.Phone);

    public override string ToString() => $"{Record.Value}, {AddressStructure.AddressLine}";
}

internal sealed class HeaderCorporationJsonConverter : JsonConverter<HeaderCorporation>
{
    public override HeaderCorporation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, HeaderCorporation value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new HeaderCorporationJson(value), options);
    }
}

public class HeaderCorporationJson(HeaderCorporation headerCorporation) : GedcomJson
{
    public AddressJson? Address { get; set; } = JsonRecord(new AddressJson(headerCorporation.AddressStructure));
    public List<string>? Emails { get; set; } = JsonList(headerCorporation.AddressEmails);
    public List<string>? FaxNumbers { get; set; } = JsonList(headerCorporation.AddressFaxNumbers);
    public List<string>? PhoneNumbers { get; set; } = JsonList(headerCorporation.PhoneNumbers);
    public List<string>? WebPages { get; set; } = JsonList(headerCorporation.AddressWebPages);
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