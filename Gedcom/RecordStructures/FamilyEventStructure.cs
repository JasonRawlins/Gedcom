using Gedcom.Entities;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
//[JsonConverter(typeof(FamilyEventJsonConverter))]
public class FamilyEventStructure : EventStructure
{
    public FamilyEventStructure() { }
    public FamilyEventStructure(Record record) : base(record) { }

    public override EventType EventType => EventType.Family;

    public static bool IsFamilyEventStructure(Record record)
    {
        var familyEventTags = new string[]
        {
                Tag.Annulment,
                Tag.Census,
                Tag.Divorce,
                Tag.DivorceFiled,
                Tag.Engagement,
                Tag.MarriageBann,
                Tag.MarriageContract,
                Tag.Marriage,
                Tag.MarriageLicense,
                Tag.MarriageSettlement,
                Tag.Residence
        };

        return familyEventTags.Contains(record.Tag);
    }
}

//internal class FamilyEventJsonConverter : JsonConverter<FamilyEventStructure>
//{
//    public override FamilyEventStructure? ReadJson(JsonReader reader, Type objectType, FamilyEventStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

//    public override void WriteJson(JsonWriter writer, FamilyEventStructure? familyEventStructure, JsonSerializer serializer)
//    {
//        if (familyEventStructure == null) throw new ArgumentNullException(nameof(familyEventStructure));

//        serializer.Serialize(writer, new FamilyEventJson(familyEventStructure));
//    }
//}

//public class FamilyEventJson(FamilyEventStructure familyEventStructure) : GedcomJson
//{
//    public AddressJson? AddressStructure { get; set; } = JsonRecord(new AddressJson(familyEventStructure.AddressStructure));
//    public string? AgeAtEvent { get; set; } = JsonString(familyEventStructure.AgeAtEvent);
//    public string? CauseOfEvent { get; set; } = JsonString(familyEventStructure.CauseOfEvent);
//    public GedcomDateJson? Date { get; set; } = JsonRecord(new GedcomDateJson(familyEventStructure.GedcomDate));
//    public string? EventOrFactClassification { get; set; } = JsonString(familyEventStructure.EventOrFactClassification);
//    public EventType EventType { get; set; } = familyEventStructure.EventType;
//    public List<MultimediaLinkJson>? MultimediaLinks { get; set; } = JsonList(familyEventStructure.MultimediaLinks.Select(ml => new MultimediaLinkJson(ml)).ToList());
//    public string? Name { get; set; } = JsonString(familyEventStructure.Name);
//    public List<string>? Notes { get; set; } = JsonList(familyEventStructure.NoteStructures.Select(ns => ns.Text).ToList());
//    public PlaceJson? Place { get; set; } = JsonRecord(new PlaceJson(familyEventStructure.PlaceStructure));
//    public string? ReligiousAffiliation { get; set; } = JsonString(familyEventStructure.ReligiousAffiliation);
//    public string? ResponsibleAgency { get; set; } = JsonString(familyEventStructure.ResponsibleAgency);
//    public string? RestrictionNotice { get; set; } = JsonString(familyEventStructure.RestrictionNotice);
//    public List<SourceCitationJson>? SourceCitations { get; set; } = JsonList(familyEventStructure.SourceCitations.Select(sc => new SourceCitationJson(sc)).ToList());
//}

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