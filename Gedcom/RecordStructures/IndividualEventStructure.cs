using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(IndividualEventStructureJsonConverter))]
public class IndividualEventStructure : RecordStructureBase, IEventDetail
{
    public IndividualEventStructure() { }
    public IndividualEventStructure(Record record) : base(record) { }

    public ChildToFamilyLink ChildToFamilyLink => First<ChildToFamilyLink>(global::Gedcom.Tag.FamilyChild);
    public AddressStructure AddressStructure => First<AddressStructure>(global::Gedcom.Tag.Address);
    public string AgeAtEvent => GetValue(global::Gedcom.Tag.Age);
    public string CauseOfEvent => GetValue(global::Gedcom.Tag.Cause);
    public string DateValue => GetValue(global::Gedcom.Tag.Date);
    public string EventOrFactClassification => GetValue(global::Gedcom.Tag.Type);
    public GedcomDate GedcomDate => GedcomDate.Parse(DateValue);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(global::Gedcom.Tag.Object);
    public string Name
    {
        get
        {
            return Tag switch
            {
                global::Gedcom.Tag.Adoption => "Adoption",
                global::Gedcom.Tag.BaptismLds => "Baptism (LDS)",
                global::Gedcom.Tag.Baptism => "Baptism",
                global::Gedcom.Tag.BarMitzvah => "Bar Mitzvah",
                global::Gedcom.Tag.BasMitzvah => "Bas Mitzvah",
                global::Gedcom.Tag.Birth => "Birth",
                global::Gedcom.Tag.Blessing => "Blessing",
                global::Gedcom.Tag.Burial => "Burial",
                global::Gedcom.Tag.Census => "Census",
                global::Gedcom.Tag.Christening => "Christening",
                global::Gedcom.Tag.AdultChristening => "Christening (Adult)",
                global::Gedcom.Tag.Confirmation => "Confirmation (LDS)",
                global::Gedcom.Tag.Cremation => "Cremation",
                global::Gedcom.Tag.Death => "Death",
                global::Gedcom.Tag.Divorce => "Divorce",
                global::Gedcom.Tag.DivorceFiled => "Divorce Filed",
                global::Gedcom.Tag.Emigration => "Emigration",
                global::Gedcom.Tag.Endowment => "Endowment",
                global::Gedcom.Tag.Engagement => "Engagement",
                global::Gedcom.Tag.Event => "Event",
                global::Gedcom.Tag.FirstCommunion => "First Communion",
                global::Gedcom.Tag.Graduation => "Graduation",
                global::Gedcom.Tag.Immigration => "Immigration",
                global::Gedcom.Tag.MarriageBann => "Marriage Bann",
                global::Gedcom.Tag.MarriageContract => "Marriage Contract",
                global::Gedcom.Tag.MarriageLicense => "Marriage License",
                global::Gedcom.Tag.Marriage => "Marriage",
                global::Gedcom.Tag.MarriageSettlement => "Marriage Settlement",
                global::Gedcom.Tag.Naturalization => "Naturalization",
                global::Gedcom.Tag.Occupation => "Occupations",
                global::Gedcom.Tag.Ordinance => "Ordinance",
                global::Gedcom.Tag.Ordination => "Ordination",
                global::Gedcom.Tag.Probate => "Probate",
                global::Gedcom.Tag.Residence => "Residence",
                global::Gedcom.Tag.Retirement => "Retirement",
                global::Gedcom.Tag.SealingChild => "Sealing (Child)",
                global::Gedcom.Tag.SealingSpouse => "Sealing (Spouse)",
                global::Gedcom.Tag.Will => "Will",
                _ => Tag,
            };
        }
    }
    public List<NoteStructure> NoteStructures => List<NoteStructure>(global::Gedcom.Tag.Note);
    public PlaceStructure PlaceStructure => First<PlaceStructure>(global::Gedcom.Tag.Place);
    public string ReligiousAffiliation => GetValue(global::Gedcom.Tag.Religion);
    public string ResponsibleAgency => GetValue(global::Gedcom.Tag.Agency);
    public string RestrictionNotice => GetValue(global::Gedcom.Tag.Restriction);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(global::Gedcom.Tag.Source);
    public string Tag => Record.Tag;

    public static bool IsIndividualEventStructure(Record record)
    {
        var individualEventTags = new string[]
        {
            "ADOP", "BAPL", "BAPM", "BARM", "BASM", "BIRT", "BLES", "BURI",
            "CENS", "CHR", "CHRA", "CONF", "CREM", "DEAT", "DIV", "DIVF",
            "EMIG", "ENDL", "ENGA", "EVEN", "FCOM", "GRAD", "IMMI", "MARB",
            "MARC", "MARL", "MARR", "MARS", "NATU", "OCCU", "ORDI", "ORDN",
            "PROB", "RESI", "RETI", "SLGC", "SLGS", "WILL"
        };

        return individualEventTags.Contains(record.Tag);
    }

    public override string ToString() => $"{Record.Value}, {ResponsibleAgency}";
}

internal class IndividualEventStructureJsonConverter : JsonConverter<IndividualEventStructure>
{
    public override IndividualEventStructure? ReadJson(JsonReader reader, Type objectType, IndividualEventStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, IndividualEventStructure? individualEventStructure, JsonSerializer serializer)
    {
        if (individualEventStructure == null) throw new ArgumentNullException(nameof(individualEventStructure));

        serializer.Serialize(writer, new IndividualEventStructureJson(individualEventStructure));
    }
}

internal class IndividualEventStructureJson : GedcomJson
{
    public IndividualEventStructureJson(IndividualEventStructure individualEventStructure)
    {
        Address = JsonRecord(individualEventStructure.AddressStructure);
        AgeAtEvent = JsonString(individualEventStructure.AgeAtEvent);
        CauseOfEvent = JsonString(individualEventStructure.CauseOfEvent);
        Date = JsonString(individualEventStructure.DateValue);
        EventOrFactClassification = JsonString(individualEventStructure.EventOrFactClassification);
        GedcomDate = individualEventStructure.GedcomDate;
        MultimediaLinks = JsonList(individualEventStructure.MultimediaLinks);
        Notes = JsonList(individualEventStructure.NoteStructures);
        Place = JsonRecord(individualEventStructure.PlaceStructure);
        ReligiousAffiliation = JsonString(individualEventStructure.ReligiousAffiliation);
        ResponsibleAgency = JsonString(individualEventStructure.ResponsibleAgency);
        RestrictionNotice = JsonString(individualEventStructure.RestrictionNotice);
        SourceCitations = JsonList(individualEventStructure.SourceCitations);
        Tag = JsonString(individualEventStructure.Tag);

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

#region INDIVIDUAL_EVENT_STRUCTURE p. 34
/* 

INDIVIDUAL_EVENT_STRUCTURE:=

[
n [ BIRT | CHR ] [Y|<NULL>] {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    +1 FAMC @<XREF:FAM>@ {0:1} p.24
|
n DEAT [Y|<NULL>] {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
|
n [ BURI | CREM ] {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
|
n ADOP {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    +1 FAMC @<XREF:FAM>@ {0:1} p.24
        +2 ADOP <ADOPTED_BY_WHICH_PARENT> {0:1} p.42
|
n [ BAPM | BARM | BASM | BLES ] {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
|
n [ CHRA | CONF | FCOM | ORDN ] {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
|
n [ NATU | EMIG | IMMI ] {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
|
n [ CENS | PROB | WILL] {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
|
n [ GRAD | RETI ] {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
|
n EVEN {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
]

As a general rule, events are things that happen on a specific date. Use the date form ‘BET date
AND date’ to indicate that an event took place at some time between two dates. Resist the
temptation to use a ‘FROM date TO date’ form in an event structure. If the subject of your
recording occurred over a period of time, then it is probably not an event, but rather an attribute or
fact.

The EVEN tag in this structure is for recording general events that are not shown in the above
<<INDIVIDUAL_EVENT_STRUCTURE>>. The event indicated by this general EVEN tag is
defined by the value of the subordinate TYPE tag. For example, a person that signed a lease for land
dated October 2, 1837 and a lease for equipment dated November 4, 1837 would be written in
GEDCOM as:

    1 EVEN
        2 TYPE Land Lease
        2 DATE 2 OCT 1837
    1 EVEN
        2 TYPE Equipment Lease
        2 DATE 4 NOV 1837

The TYPE tag can be optionally used to modify the basic understanding of its superior event or
attribute. For example:

    1 GRAD
        2 TYPE College

The occurrence of an event is asserted by the presence of either a DATE tag and value or a PLACe
tag and value in the event structure. When neither the date value nor the place value are known then
a Y(es) value on the parent event tag line is required to assert that the event happened. For example
each of the following GEDCOM structures assert that a death happened:

    1 DEAT Y
    1 DEAT
        2 DATE 2 OCT 1937
    1 DEAT
        2 PLAC Cove, Cache, Utah

Using this convention, as opposed to the just the presence of the tag, protects GEDCOM processors
which removes (prunes) lines which have neither a value nor any subordinate line. It also allows a
note or source to be attached to an event context without implying that the event occurred.

It is not proper GEDCOM form to use a N(o) value with an event tag to infer that it did not happen.
A convention to handle events which never happened may be defined in the future.

*/
#endregion