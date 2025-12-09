using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(AddressStructureJsonConverter))]
public class AddressStructure : RecordStructureBase
{
    public AddressStructure() { }
    public AddressStructure(Record record) : base(record) { }

    public string AddressCity => _(Tag.CITY);
    public string AddressCountry => _(Tag.CTRY);
    public string AddressLine => Record.Value;
    public string AddressLine1 => _(Tag.ADR1);
    public string AddressLine2 => _(Tag.ADR2);
    public string AddressLine3 => _(Tag.ADR3);
    public string AddressPostCode => _(Tag.POST);
    public string AddressState => _(Tag.STAE);

    public override string ToString() => $"{Record.Value}, {AddressLine}";
}

internal class AddressStructureJsonConverter : JsonConverter<AddressStructure>
{
    public override AddressStructure? ReadJson(JsonReader reader, Type objectType, AddressStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, AddressStructure? addressStructure, JsonSerializer serializer)
    {
        if (addressStructure == null) throw new ArgumentNullException(nameof(addressStructure));

        serializer.Serialize(writer, new AddressStructureJson(addressStructure));
    }
}

internal class AddressStructureJson : GedcomJson
{
    public AddressStructureJson(AddressStructure addressStructure)
    {
        AddressCity = JsonString(addressStructure.AddressCity);
        AddressCountry = JsonString(addressStructure.AddressCountry);
        AddressLine = JsonString(addressStructure.AddressLine);
        AddressLine1 = JsonString(addressStructure.AddressLine1);
        AddressLine2 = JsonString(addressStructure.AddressLine2);
        AddressLine3 = JsonString(addressStructure.AddressLine3);
        AddressPostCode = JsonString(addressStructure.AddressPostCode);
        AddressState = JsonString(addressStructure.AddressState);
    }

    public string? AddressCity { get; set; }
    public string? AddressCountry { get; set; }
    public string? AddressLine { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressPostCode { get; set; }
    public string? AddressState { get; set; }
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