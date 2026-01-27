using Gedcom.Core;
using Gedcom.Entities;
using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(EventJsonConverter))]
public class EventStructure : RecordStructureBase, IComparable<EventStructure>
{
    public EventStructure() { }
    public EventStructure(Record record) : base(record) { }

    public AddressStructure AddressStructure => First<AddressStructure>(Tag.Address);
    public string AgeAtEvent => GetValue(Tag.Age);
    public string CauseOfEvent => GetValue(Tag.Cause);
    public ChildToFamilyLink ChildToFamilyLink => First<ChildToFamilyLink>(Tag.FamilyChild);
    public string DateValue => GetValue(Tag.Date);
    public string EventOrFactClassification => GetValue(Tag.Type);
    public virtual EventType EventType { get; set; }
    public GedcomDate GedcomDate => GedcomDate.Parse(DateValue);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(Tag.Object);
    public string Name
    {
        get
        {
            return Record.Tag switch
            {
                Tag.Adoption => "Adoption",
                Tag.BaptismLds => "Baptism (LDS)",
                Tag.Baptism => "Baptism",
                Tag.BarMitzvah => "Bar Mitzvah",
                Tag.BasMitzvah => "Bas Mitzvah",
                Tag.Birth => "Birth",
                Tag.Blessing => "Blessing",
                Tag.Burial => "Burial",
                Tag.Census => "Census",
                Tag.Christening => "Christening",
                Tag.AdultChristening => "Christening (Adult)",
                Tag.Confirmation => "Confirmation (LDS)",
                Tag.Cremation => "Cremation",
                Tag.Death => "Death",
                Tag.Divorce => "Divorce",
                Tag.DivorceFiled => "Divorce Filed",
                ExtensionTag.Election => "Election",
                Tag.Emigration => "Emigration",
                Tag.Endowment => "Endowment",
                Tag.Engagement => "Engagement",
                Tag.Event => "Event",
                Tag.FirstCommunion => "First Communion",
                Tag.Graduation => "Graduation",
                Tag.Immigration => "Immigration",
                Tag.MarriageBann => "Marriage Bann",
                Tag.MarriageContract => "Marriage Contract",
                Tag.MarriageLicense => "Marriage License",
                Tag.Marriage => "Marriage",
                Tag.MarriageSettlement => "Marriage Settlement",
                Tag.Naturalization => "Naturalization",
                Tag.Occupation => "Occupation",
                Tag.Ordinance => "Ordinance",
                Tag.Ordination => "Ordination",
                Tag.Probate => "Probate",
                Tag.Residence => "Residence",
                Tag.Retirement => "Retirement",
                Tag.SealingChild => "Sealing (Child)",
                Tag.SealingSpouse => "Sealing (Spouse)",
                Tag.Will => "Will",
                Tag.Annulment => "Annulment",
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

    public int CompareTo(EventStructure? other)
    {
        if (other == null) return 1;

        // Compare by year first, then month, then day
        int yearComparison = GedcomDate.Year.CompareTo(other.GedcomDate.Year);
        if (yearComparison != 0) return yearComparison;

        int monthComparison = GedcomDate.Month.CompareTo(other.GedcomDate.Month);
        if (monthComparison != 0) return monthComparison;

        return GedcomDate.Day.CompareTo(other.GedcomDate.Day);
    }

    public override string ToString() => $"{Name}, {DateValue}";
}

internal class EventJsonConverter : JsonConverter<EventStructure>
{
    public override EventStructure? ReadJson(JsonReader reader, Type objectType, EventStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, EventStructure? eventStructure, JsonSerializer serializer)
    {
        if (eventStructure == null) throw new ArgumentNullException(nameof(eventStructure));

        serializer.Serialize(writer, new EventJson(eventStructure));
    }
}

public class EventJson(EventStructure eventStructure) : GedcomJson, IComparable<EventJson>
{
    public AddressJson? Address { get; set; } = JsonRecord(new AddressJson(eventStructure.AddressStructure));
    public string? AgeAtEvent { get; set; } = JsonString(eventStructure.AgeAtEvent);
    public string? CauseOfEvent { get; set; } = JsonString(eventStructure.CauseOfEvent);
    public string? EventOrFactClassification { get; set; } = JsonString(eventStructure.EventOrFactClassification);
    public GedcomDateJson Date { get; set; } = new GedcomDateJson(eventStructure.GedcomDate);
    public List<MultimediaLinkJson>? MultimediaLinks { get; set; } = JsonList(eventStructure.MultimediaLinks.Select(ml => new MultimediaLinkJson(ml)).ToList());
    public string? Name { get; set; } = JsonString(eventStructure.Name);
    public List<string>? Notes { get; set; } = JsonList(eventStructure.NoteStructures.Select(ns => ns.Text).ToList());
    public PlaceJson? Place { get; set; } = JsonRecord(new PlaceJson(eventStructure.PlaceStructure));
    public string? ReligiousAffiliation { get; set; } = JsonString(eventStructure.ReligiousAffiliation);
    public string? ResponsibleAgency { get; set; } = JsonString(eventStructure.ResponsibleAgency);
    public string? RestrictionNotice { get; set; } = JsonString(eventStructure.RestrictionNotice);
    public List<SourceCitationJson>? SourceCitations { get; set; } = JsonList(eventStructure.SourceCitations.Select(sc => new SourceCitationJson(sc)).ToList());

    public int CompareTo(EventJson? other)
    {
        if (other == null) return 1;

        // Compare by year first, then month, then day
        int yearComparison = Nullable.Compare(Date.Year, other.Date.Year);
        if (yearComparison != 0) return yearComparison;

        int monthComparison = Nullable.Compare(Date.Month, other.Date.Month);
        if (monthComparison != 0) return monthComparison;

        return Nullable.Compare(Date.Day, other.Date.Day);
    }

    public override string ToString() => $"{Name} {Date}";
}

#region Event structure
/* 

This is the base class for IndividualEventStructure and FamilyEventStructure. They are
almost always used identically. See either of these classes to view their documentation.

*/
#endregion