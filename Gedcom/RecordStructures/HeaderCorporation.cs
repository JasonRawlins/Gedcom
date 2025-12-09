using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(HeaderCorporationJsonConverter))]
public class HeaderCorporation : RecordStructureBase, IAddressStructure
{
    public HeaderCorporation() : base() { }
    public HeaderCorporation(Record record) : base(record) { }

    public List<string> AddressEmails => ListValues(Tag.EMAIL);
    public List<string> AddressFaxNumbers => ListValues(Tag.FAX);
    public AddressStructure AddressStructure => First<AddressStructure>(Tag.ADDR);
    public List<string> AddressWebPages => ListValues(Tag.WWW);
    public List<string> PhoneNumbers => ListValues(Tag.PHON);

    public override string ToString() => $"{Record.Value}, {AddressStructure.AddressLine}";
}

internal class HeaderCorporationJsonConverter : JsonConverter<HeaderCorporation>
{
    public override HeaderCorporation? ReadJson(JsonReader reader, Type objectType, HeaderCorporation? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, HeaderCorporation? headerCorporation, JsonSerializer serializer)
    {
        if (headerCorporation == null) throw new ArgumentNullException(nameof(headerCorporation));

        serializer.Serialize(writer, new HeaderCorporationJson(headerCorporation));
    }
}

internal class HeaderCorporationJson : GedcomJson
{
    public HeaderCorporationJson(HeaderCorporation headerCorporation)
    {
        AddressEmails = JsonList(headerCorporation.AddressEmails);
        AddressFaxNumbers = JsonList(headerCorporation.AddressFaxNumbers);
        AddressStructure = JsonRecord(headerCorporation.AddressStructure);
        AddressWebPages = JsonList(headerCorporation.AddressWebPages);
        PhoneNumbers = JsonList(headerCorporation.PhoneNumbers);
    }

    public List<string>? AddressEmails { get; set; }
    public List<string>? AddressFaxNumbers { get; set; }
    public AddressStructure? AddressStructure { get; set; }
    public List<string>? AddressWebPages { get; set; }
    public List<string>? PhoneNumbers { get; set; }
}

#region HeaderCorporation p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 CORP <NAME_OF_BUSINESS> {0:1} p.54
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31

*/
#endregion