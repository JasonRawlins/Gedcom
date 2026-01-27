using Gedcom.Core;
using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(AddressJsonConverter))]
public class AddressStructure : RecordStructureBase
{
    public AddressStructure() { }
    public AddressStructure(Record record) : base(record) { }

    public string AddressCity => GetValue(Tag.City);
    public string AddressCountry => GetValue(Tag.Country);
    public string AddressLine => Record.Value;
    public string AddressLine1 => GetValue(Tag.Address1);
    public string AddressLine2 => GetValue(Tag.Address2);
    public string AddressLine3 => GetValue(Tag.Address3);
    public string AddressPostCode => GetValue(Tag.PostalCode);
    public string AddressState => GetValue(Tag.State);

    public override string ToString() => $"{Record.Value}, {AddressLine}";
}

internal class AddressJsonConverter : JsonConverter<AddressStructure>
{
    public override AddressStructure? ReadJson(JsonReader reader, Type objectType, AddressStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, AddressStructure? addressStructure, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(addressStructure);
        serializer.Serialize(writer, new AddressJson(addressStructure));
    }
}

public class AddressJson(AddressStructure addressStructure) : GedcomJson
{
    public string? City { get; set; } = JsonString(addressStructure.AddressCity);
    public string? Country { get; set; } = JsonString(addressStructure.AddressCountry);
    public string? Line { get; set; } = JsonString(addressStructure.AddressLine);
    public string? Line1 { get; set; } = JsonString(addressStructure.AddressLine1);
    public string? Line2 { get; set; } = JsonString(addressStructure.AddressLine2);
    public string? Line3 { get; set; } = JsonString(addressStructure.AddressLine3);
    public string? PostalCode { get; set; } = JsonString(addressStructure.AddressPostCode);
    public string? State { get; set; } = JsonString(addressStructure.AddressState);

    public override string ToString() => $"{Line}, {City}, {State}, {PostalCode}";
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