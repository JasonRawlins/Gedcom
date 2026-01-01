using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
// This class is not being used in code. See Note 1 in FamilyRecord.cs for the reason.
// It is being left in because it mirrors the structure from the specification. This
// discrepancy may be a misunderstanding on my part.
[JsonConverter(typeof(FamilyEventJsonConverter))]
public class FamilyEventStructure : RecordStructureBase
{
    public FamilyEventStructure() { }
    public FamilyEventStructure(Record record) : base(record) { }

    public FamilyEventDetail FamilyEventDetail => First<FamilyEventDetail>(Tag.Date);

    public override string ToString() => $"{Record.Value}, {FamilyEventDetail.Husband}, {FamilyEventDetail.Wife}";
}

internal class FamilyEventJsonConverter : JsonConverter<FamilyEventStructure>
{
    public override FamilyEventStructure? ReadJson(JsonReader reader, Type objectType, FamilyEventStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, FamilyEventStructure? familyEventStructure, JsonSerializer serializer)
    {
        if (familyEventStructure == null) throw new ArgumentNullException(nameof(familyEventStructure));

        serializer.Serialize(writer, new FamilyEventJson(familyEventStructure));
    }
}

internal class FamilyEventJson : GedcomJson
{
    public FamilyEventJson(FamilyEventStructure familyEventStructure)
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