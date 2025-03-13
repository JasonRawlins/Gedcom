using Gedcom.Core;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(HeaderCORPJsonConverter))]
public class HeaderCORP : RecordStructureBase, IAddressStructure
{
    public HeaderCORP() : base() { }
    public HeaderCORP(Record record) : base(record) { }

    public AddressStructure AddressStructure => First<AddressStructure>(C.ADDR);

    #region IAddressStructure

    public List<string> PhoneNumbers => ListValues(C.PHON);
    public List<string> AddressEmails => ListValues(C.EMAIL);
    public List<string> AddressFaxNumbers => ListValues(C.FAX);
    public List<string> AddressWebPages => ListValues(C.WWW);

    #endregion
}

internal class HeaderCORPJsonConverter : JsonConverter<HeaderCORP>
{
    public override HeaderCORP? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderCORP headerCORP, JsonSerializerOptions options)
    {
        var headerCORPJson = new HeaderCORPJson(headerCORP);
        JsonSerializer.Serialize(writer, headerCORPJson, headerCORPJson.GetType(), options);
    }
}

internal class HeaderCORPJson : GedcomJson
{
    public HeaderCORPJson(HeaderCORP headerCORP)
    {
        AddressStructure = JsonRecord(headerCORP.AddressStructure);
        PhoneNumbers = JsonList(headerCORP.PhoneNumbers);
        AddressEmails = JsonList(headerCORP.AddressEmails);
        AddressFaxNumbers = JsonList(headerCORP.AddressFaxNumbers);
        AddressWebPages = JsonList(headerCORP.AddressWebPages);
    }

    public AddressStructure? AddressStructure { get; set; }

    #region IAddressStructure

    public List<string>? PhoneNumbers { get; set; }
    public List<string>? AddressEmails { get; set; }
    public List<string>? AddressFaxNumbers { get; set; }
    public List<string>? AddressWebPages { get; set; }

    #endregion
}

#region HeaderCORP p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 CORP <NAME_OF_BUSINESS> {0:1} p.54
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31

*/
#endregion