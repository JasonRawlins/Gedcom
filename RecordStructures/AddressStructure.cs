using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(AddressStructureJsonConverter))]
public class AddressStructure : RecordStructureBase
{
    public AddressStructure() { }
    public AddressStructure(Record record) : base(record) { }

    public string AddressLine => Record.Value;
    public string AddressLine1 => _(C.ADR1);
    public string AddressLine2 => _(C.ADR2);
    public string AddressLine3 => _(C.ADR3);
    public string AddressCity => _(C.CITY);
    public string AddressState => _(C.STAE);
    public string AddressPostCode => _(C.POST);
    public string AddressCountry => _(C.CTRY);
}

public interface IAddressStructure
{
    List<string> PhoneNumbers { get; }
    List<string> AddressEmails { get; }
    List<string> AddressFaxNumbers { get; }
    List<string> AddressWebPages { get; }
}

internal class AddressStructureJsonConverter : JsonConverter<AddressStructure>
{
    public override AddressStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, AddressStructure addressStructure, JsonSerializerOptions options)
    {
        var addressStructureJson = new AddressStructureJson(addressStructure);
        JsonSerializer.Serialize(writer, addressStructureJson, addressStructureJson.GetType(), options);
    }
}

internal class AddressStructureJson
{
    public AddressStructureJson(AddressStructure addressStructure)
    {
        AddressLine = string.IsNullOrEmpty(addressStructure.AddressLine) ? null : addressStructure.AddressLine;
        AddressLine1 = string.IsNullOrEmpty(addressStructure.AddressLine1) ? null : addressStructure.AddressLine1;
        AddressLine2 = string.IsNullOrEmpty(addressStructure.AddressLine2) ? null : addressStructure.AddressLine2;
        AddressLine3 = string.IsNullOrEmpty(addressStructure.AddressLine3) ? null : addressStructure.AddressLine3;
        AddressCity = string.IsNullOrEmpty(addressStructure.AddressCity) ? null : addressStructure.AddressCity;
        AddressState = string.IsNullOrEmpty(addressStructure.AddressState) ? null : addressStructure.AddressState;
        AddressPostCode = string.IsNullOrEmpty(addressStructure.AddressPostCode) ? null : addressStructure.AddressPostCode;
        AddressCountry = string.IsNullOrEmpty(addressStructure.AddressCountry) ? null : addressStructure.AddressCountry;
    }

    public string? AddressLine { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressCity { get; set; }
    public string? AddressState { get; set; }
    public string? AddressPostCode { get; set; }
    public string? AddressCountry { get; set; }
}

#region ADDRESS_STRUCTURE p. 31
/* 

ADDRESS_STRUCTURE:=

n ADDR <ADDRESS_LINE> {1:1} p.41
    +1 CONT <ADDRESS_LINE> {0:3} p.41
    +1 ADR1 <ADDRESS_LINE1> {0:1} p.41
    +1 ADR2 <ADDRESS_LINE2> {0:1} p.41
    +1 ADR3 <ADDRESS_LINE3> {0:1} p.41
    +1 CITY <ADDRESS_CITY> {0:1} p.41
    +1 STAE <ADDRESS_STATE> {0:1} p.42
    +1 POST <ADDRESS_POSTAL_CODE> {0:1} p.41
    +1 CTRY <ADDRESS_COUNTRY> {0:1} p.41
n PHON <PHONE_NUMBER> {0:3} p.57
n EMAIL <ADDRESS_EMAIL> {0:3} p.41
n FAX <ADDRESS_FAX> {0:3} p.41
n WWW <ADDRESS_WEB_PAGE> {0:3} p.42

The address structure should be formed as it would appear on a mailing label using the ADDR and
the CONT lines to form the address structure. The ADDR and CONT lines are required for any
address. The additional subordinate address tags such as STAE and CTRY are provided to be used
by systems that have structured their addresses for indexing and sorting. For backward compatibility
these lines are not to be used in lieu of the required ADDR.and CONT line structure.

*/
#endregion