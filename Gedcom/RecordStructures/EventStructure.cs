using System.Text.Json;
using System.Text.Json.Serialization;
using Gedcom.Entities;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(EventStructureJsonConverter))]
public class EventStructure : RecordStructureBase, IComparable<EventStructure>
{
    public EventStructure() { }
    public EventStructure(Record record) : base(record) { }

    private AddressStructure? addressStructure = null;
    public AddressStructure AddressStructure => addressStructure ??= First<AddressStructure>(Tag.Address);

    private string? ageAtEvent = null;
    public string AgeAtEvent => ageAtEvent ??= GetValue(Tag.Age);

    private string? causeOfEvent = null;
    public string CauseOfEvent => causeOfEvent ??= GetValue(Tag.Cause);

    private ChildToFamilyLink? childToFamilyLink = null;
    public ChildToFamilyLink ChildToFamilyLink => childToFamilyLink ??= First<ChildToFamilyLink>(Tag.FamilyChild);

    private string? dateValue = null;
    public string DateValue => dateValue ??= GetValue(Tag.Date);

    private string? eventOrFactClassification = null;
    public string EventOrFactClassification => eventOrFactClassification ??= GetValue(Tag.Type);
   
    public virtual EventType EventType { get; set; }

    private GedcomDate? gedcomDate = null;
    public GedcomDate GedcomDate => gedcomDate ??= GedcomDate.Parse(DateValue);

    private List<MultimediaLink>? multimediaLinks = null;
    public List<MultimediaLink> MultimediaLinks => multimediaLinks ??= List<MultimediaLink>(Tag.Object);
    
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

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    private PlaceStructure? placeStructure = null;
    public PlaceStructure PlaceStructure => placeStructure ??= First<PlaceStructure>(Tag.Place);

    private string? religiousAffiliation = null;
    public string ReligiousAffiliation => religiousAffiliation ??= GetValue(Tag.Religion);

    private string? responsibleAgency = null;
    public string ResponsibleAgency => responsibleAgency ??= GetValue(Tag.Agency);

    private string? restrictionNotice = null;
    public string RestrictionNotice => restrictionNotice ??= GetValue(Tag.Restriction);

    private List<SourceCitation>? sourceCitations = null;
    public List<SourceCitation> SourceCitations => sourceCitations ??= List<SourceCitation>(Tag.Source);

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

internal sealed class EventStructureJsonConverter : JsonConverter<EventStructure>
{
    public override EventStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, EventStructure value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new EventJson(value), options);
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