using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(FamilyEventStructureJsonConverter))]
public class FamilyEventStructure : RecordStructureBase
{
    public FamilyEventStructure() { }
    public FamilyEventStructure(Record record) : base(record) { }

    public FamilyEventDetail FamilyEventDetail => First<FamilyEventDetail>(Record.Tag);

    public override string ToString() => $"{Record.Value}, {FamilyEventDetail.Husband}, {FamilyEventDetail.Wife}";
}

internal class FamilyEventStructureJsonConverter : JsonConverter<FamilyEventStructure>
{
    public override FamilyEventStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, FamilyEventStructure familyEventStructure, JsonSerializerOptions options)
    {
        var familyEventStructureJson = new FamilyEventStructureJson(familyEventStructure);
        JsonSerializer.Serialize(writer, familyEventStructureJson, familyEventStructureJson.GetType(), options);
    }
}

internal class FamilyEventStructureJson : GedcomJson
{
    public FamilyEventStructureJson(FamilyEventStructure familyEventStructure)
    {
        FamilyEventDetail = JsonRecord(familyEventStructure.FamilyEventDetail);
    }

    public FamilyEventDetail? FamilyEventDetail { get; set; }

}

#region FAMILY_EVENT_STRUCTURE p. 32
/* 

FAMILY_EVENT_STRUCTURE:=

[
n [ ANUL | CENS | DIV | DIVF ] {1:1}
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n [ ENGA | MARB | MARC ] {1:1}
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n MARR [Y|<NULL>] {1:1}
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n [ MARL | MARS ] {1:1}
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n RESI
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
|
n EVEN [<EVENT_DESCRIPTOR> | <NULL>] {1:1} p.48
    +1 <<FAMILY_EVENT_DETAIL>> {0:1} p.32
]

*/
#endregion