using System.Text.Json.Serialization;
using System.Text.Json;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(HeaderCorporationJsonConverter))]
public class HeaderCorporation : RecordStructureBase, IAddressStructure
{
    public HeaderCorporation() : base() { }
    public HeaderCorporation(Record record) : base(record) { }

    public AddressStructure AddressStructure => First<AddressStructure>(C.ADDR);

    #region IAddressStructure

    public List<string> PhoneNumbers => ListValues(C.PHON);
    public List<string> AddressEmails => ListValues(C.EMAIL);
    public List<string> AddressFaxNumbers => ListValues(C.FAX);
    public List<string> AddressWebPages => ListValues(C.WWW);

    #endregion
}

internal class HeaderCorporationJsonConverter : JsonConverter<HeaderCorporation>
{
    public override HeaderCorporation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderCorporation headerCorporation, JsonSerializerOptions options)
    {
        var headerCorporationJson = new HeaderCorporationJson(headerCorporation);
        JsonSerializer.Serialize(writer, headerCorporationJson, headerCorporationJson.GetType(), options);
    }
}

internal class HeaderCorporationJson : GedcomJson
{
    public HeaderCorporationJson(HeaderCorporation headerCorporation)
    {
        AddressStructure = JsonRecord(headerCorporation.AddressStructure);
        PhoneNumbers = JsonList(headerCorporation.PhoneNumbers);
        AddressEmails = JsonList(headerCorporation.AddressEmails);
        AddressFaxNumbers = JsonList(headerCorporation.AddressFaxNumbers);
        AddressWebPages = JsonList(headerCorporation.AddressWebPages);
    }

    public AddressStructure? AddressStructure { get; set; }

    #region IAddressStructure

    public List<string>? PhoneNumbers { get; set; }
    public List<string>? AddressEmails { get; set; }
    public List<string>? AddressFaxNumbers { get; set; }
    public List<string>? AddressWebPages { get; set; }

    #endregion
}

#region HeaderCorporation p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 CORP <NAME_OF_BUSINESS> {0:1} p.54
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31

*/
#endregion