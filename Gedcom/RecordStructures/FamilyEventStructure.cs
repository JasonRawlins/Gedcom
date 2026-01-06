using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FamilyEventJsonConverter))]
public class FamilyEventStructure : RecordStructureBase, IEventDetail
{
    public FamilyEventStructure() { }
    public FamilyEventStructure(Record record) : base(record) { }

    public AddressStructure AddressStructure => First<AddressStructure>(Tag.Address);
    public string AgeAtEvent => GetValue(Tag.Age);
    public string CauseOfEvent => GetValue(Tag.Cause);
    public string DateValue => GetValue(Tag.Date);
    public string EventOrFactClassification => GetValue(Tag.Type);
    public GedcomDate GedcomDate => GedcomDate.Parse(DateValue);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(Tag.Object);
    public string Name
    {
        get
        {
            return Record.Tag switch
            {
                Tag.Annulment => "Annulment",
                Tag.Census => "Census",
                Tag.Divorce => "Divorce",
                Tag.DivorceFiled => "Divorce Filed",
                Tag.Engagement => "Engagement",
                Tag.MarriageBann => "Marriage Bann",
                Tag.MarriageContract => "Marriage Contract",
                Tag.Marriage => "Marriage",
                Tag.MarriageLicense => "Marriage License",
                Tag.MarriageSettlement => "Marriage Settlement",
                Tag.Residence => "Residence",
                _ => Record.Tag,
            };
        }
    }

    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public PlaceStructure PlaceStructure => First<PlaceStructure>(Tag.Place);
    public string ReligiousAffiliation => GetValue(Tag.Religion);
    public string ResponsibleAgency => GetValue(Tag.Agency);
    public string RestrictionNotice => GetValue(Tag.Restriction);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);

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

    public override string ToString() => $"{Record}";
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
        Address = JsonRecord(familyEventStructure.AddressStructure);
        AgeAtEvent = JsonString(familyEventStructure.AgeAtEvent);
        CauseOfEvent = JsonString(familyEventStructure.CauseOfEvent);
        Date = JsonString(familyEventStructure.DateValue);
        EventOrFactClassification = JsonString(familyEventStructure.EventOrFactClassification);
        GedcomDate = familyEventStructure.GedcomDate;
        MultimediaLinks = JsonList(familyEventStructure.MultimediaLinks);
        Notes = JsonList(familyEventStructure.NoteStructures);
        Place = JsonRecord(familyEventStructure.PlaceStructure);
        ReligiousAffiliation = JsonString(familyEventStructure.ReligiousAffiliation);
        ResponsibleAgency = JsonString(familyEventStructure.ResponsibleAgency);
        RestrictionNotice = JsonString(familyEventStructure.RestrictionNotice);
        SourceCitations = JsonList(familyEventStructure.SourceCitations);
        Tag = JsonString(familyEventStructure.Record.Tag);
    }

    public AddressStructure? Address { get; set; }
    public string? AgeAtEvent { get; set; }
    public string? CauseOfEvent { get; set; }
    public string? Date { get; set; }
    public string? EventOrFactClassification { get; set; }
    public GedcomDate GedcomDate { get; set; }
    public List<MultimediaLink>? MultimediaLinks { get; set; }
    public List<NoteStructure>? Notes { get; set; }
    public PlaceStructure? Place { get; set; }
    public string? ReligiousAffiliation { get; set; }
    public string? ResponsibleAgency { get; set; }
    public string? RestrictionNotice { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public string? Tag { get; set; }

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