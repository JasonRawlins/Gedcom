using System.Text.Json;
using System.Text.Json.Serialization;
using Gedcom.Entities;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(EventStructureJsonConverter))]
public class EventStructure : RecordStructureBase, IComparable<EventStructure>
{
    public EventStructure() { }
    public EventStructure(Record record) : base(record) { }

    private AddressStructure? _addressStructure = null;
    public AddressStructure AddressStructure => _addressStructure ??= First<AddressStructure>(Tag.Address);

    private string? _ageAtEvent = null;
    public string AgeAtEvent => _ageAtEvent ??= GetValue(Tag.Age);

    private string? _causeOfEvent = null;
    public string CauseOfEvent => _causeOfEvent ??= GetValue(Tag.Cause);

    private ChildToFamilyLink? _childToFamilyLink = null;
    public ChildToFamilyLink ChildToFamilyLink => _childToFamilyLink ??= First<ChildToFamilyLink>(Tag.FamilyChild);

    private string? _dateValue = null;
    public string DateValue => _dateValue ??= GetValue(Tag.Date);

    private string? _eventOrFactClassification = null;
    public string EventOrFactClassification => _eventOrFactClassification ??= GetValue(Tag.Type);
   
    public virtual EventType EventType { get; set; }

    private GedcomDate? _gedcomDate = null;
    public GedcomDate GedcomDate => _gedcomDate ??= GedcomDate.Parse(DateValue);

    private List<MultimediaLink>? _multimediaLinks = null;
    public List<MultimediaLink> MultimediaLinks => _multimediaLinks ??= List<MultimediaLink>(Tag.Object);
    
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
                ExtensionTag.Ancestry.Election => "Election",
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

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private PlaceStructure? _placeStructure = null;
    public PlaceStructure PlaceStructure => _placeStructure ??= First<PlaceStructure>(Tag.Place);

    private string? _religiousAffiliation = null;
    public string ReligiousAffiliation => _religiousAffiliation ??= GetValue(Tag.Religion);

    private string? _responsibleAgency = null;
    public string ResponsibleAgency => _responsibleAgency ??= GetValue(Tag.Agency);

    private string? _restrictionNotice = null;
    public string RestrictionNotice => _restrictionNotice ??= GetValue(Tag.Restriction);

    private List<SourceCitation>? _sourceCitations = null;
    public List<SourceCitation> SourceCitations => _sourceCitations ??= List<SourceCitation>(Tag.Source);

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
        JsonSerializer.Serialize(writer, new EventDto(value), GedcomDto.SerializationOptions);
    }
}

public class EventDto(EventStructure eventStructure) : GedcomDto, IComparable<EventDto>
{
    public AddressDto? Address { get; set; } = GetRecord(new AddressDto(eventStructure.AddressStructure));
    public string? AgeAtEvent { get; set; } = GetString(eventStructure.AgeAtEvent);
    public string? CauseOfEvent { get; set; } = GetString(eventStructure.CauseOfEvent);
    public string? EventOrFactClassification { get; set; } = GetString(eventStructure.EventOrFactClassification);
    public GedcomDateDto Date { get; set; } = new GedcomDateDto(eventStructure.GedcomDate);
    public List<MultimediaLinkDto>? MultimediaLinks { get; set; } = GetList(eventStructure.MultimediaLinks.Select(ml => new MultimediaLinkDto(ml)).ToList());
    public string? Name { get; set; } = GetString(eventStructure.Name);
    public List<string>? Notes { get; set; } = GetList(eventStructure.NoteStructures.Select(ns => ns.Text).ToList());
    public PlaceDto? Place { get; set; } = GetRecord(new PlaceDto(eventStructure.PlaceStructure));
    public string? ReligiousAffiliation { get; set; } = GetString(eventStructure.ReligiousAffiliation);
    public string? ResponsibleAgency { get; set; } = GetString(eventStructure.ResponsibleAgency);
    public string? RestrictionNotice { get; set; } = GetString(eventStructure.RestrictionNotice);
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(eventStructure.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());

    public int CompareTo(EventDto? other)
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