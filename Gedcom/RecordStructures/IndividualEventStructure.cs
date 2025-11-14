using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(IndividualEventStructureJsonConverter))]
public class IndividualEventStructure : RecordStructureBase, IEventDetail
{
    public IndividualEventStructure() { }
    public IndividualEventStructure(Record record) : base(record) { }
    public ChildToFamilyLink ChildToFamilyLink => First<ChildToFamilyLink>(C.FAMC);
    public AddressStructure AddressStructure => First<AddressStructure>(C.ADDR);
    public string AgeAtEvent => _(C.AGE);
    public string CauseOfEvent => _(C.CAUS);
    public string DateValue => _(C.DATE);
    public string EventOrFactClassification => _(C.TYPE);
    public GedcomDate GedcomDate => GedcomDate.Parse(DateValue);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(C.OBJE);
    public string Name
    {
        get
        {
            return Tag switch
            {
                C.ADOP => "Adoption",
                C.BAPL => "Baptism (LDS)",
                C.BAPM => "Baptism",
                C.BARM => "Bar Mitzvah",
                C.BASM => "Bas Mitzvah",
                C.BIRT => "Birth",
                C.BLES => "Blessing",
                C.BURI => "Burial",
                C.CENS => "Census",
                C.CHR => "Christening",
                C.CHRA => "Christening (Adult)",
                C.CONF => "Confirmation (LDS)",
                C.CREM => "Cremation",
                C.DEAT => "Death",
                C.DIV => "Divorce",
                C.DIVF => "Divorce Filed",
                C.EMIG => "Emigration",
                C.ENDL => "Endowment",
                C.ENGA => "Engagement",
                C.EVEN => "Event",
                C.FCOM => "First Communion",
                C.GRAD => "Graduation",
                C.IMMI => "Immigration",
                C.MARB => "Marriage Bann",
                C.MARC => "Marriage Contract",
                C.MARL => "Marriage License",
                C.MARR => "Marriage",
                C.MARS => "Marriage Settlement",
                C.NATU => "Naturalization",
                C.OCCU => "Occupations",
                C.ORDI => "Ordinance",
                C.ORDN => "Ordination",
                C.PROB => "Probate",
                C.RESI => "Residence",
                C.RETI => "Retirement",
                C.SLGC => "Sealing (Child)",
                C.SLGS => "Sealing (Spouse)",
                C.WILL => "Will",
                _ => Tag,
            };
        }
    }
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public PlaceStructure PlaceStructure => First<PlaceStructure>(C.PLAC);
    public string ReligiousAffiliation => _(C.RELI);
    public string ResponsibleAgency => _(C.AGNC);
    public string RestrictionNotice => _(C.RESN);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
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