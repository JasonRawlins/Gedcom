using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(FamilyEventDetailJsonConverter))]
public class EventDetail : RecordStructureBase, IEventDetail
{
    public EventDetail() : base() { }
    public EventDetail(Record record) : base(record) { }

    public string EventOrFactClassification => _(C.TYPE);
    public string DateValue => _(C.DATE);
    public PlaceStructure PlaceStructure => First<PlaceStructure>(C.PLAC);
    public AddressStructure AddressStructure => First<AddressStructure>(C.ADDR);
    public string ResponsibleAgency => _(C.AGNC);
    public string ReligiousAffiliation => _(C.RELI);
    public string CauseOfEvent => _(C.CAUS);
    public string RestrictionNotice => _(C.RESN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(C.OBJE);
}

internal class EventDetailJsonConverter : JsonConverter<EventDetail>
{
    public override EventDetail? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, EventDetail multimediaLink, JsonSerializerOptions options)
    {
        var multimediaLinkJson = new EventDetailJson(multimediaLink);
        JsonSerializer.Serialize(writer, multimediaLinkJson, multimediaLinkJson.GetType(), options);
    }
}

internal class EventDetailJson : GedcomJson
{
    public EventDetailJson(EventDetail eventDetail)
    {
        EventOrFactClassification = JsonString(eventDetail.EventOrFactClassification);
        DateValue = JsonString(eventDetail.DateValue);
        PlaceStructure = JsonRecord(eventDetail.PlaceStructure);
        AddressStructure = JsonRecord(eventDetail.AddressStructure);
        ResponsibleAgency = JsonString(eventDetail.ResponsibleAgency);
        ReligiousAffiliation = JsonString(eventDetail.ReligiousAffiliation);
        CauseOfEvent = JsonString(eventDetail.CauseOfEvent);
        RestrictionNotice = JsonString(eventDetail.RestrictionNotice);
        NoteStructures = JsonList(eventDetail.NoteStructures);
        SourceCitations = JsonList(eventDetail.SourceCitations);
        MultimediaLinks = JsonList(eventDetail.MultimediaLinks);
    }

    public string? EventOrFactClassification { get; set; }
    public string? DateValue { get; set; }
    public PlaceStructure? PlaceStructure { get; set; }
    public AddressStructure? AddressStructure { get; set; }
    public string? ResponsibleAgency { get; set; }
    public string? ReligiousAffiliation { get; set; }
    public string? CauseOfEvent { get; set; }
    public string? RestrictionNotice { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public List<MultimediaLink>? MultimediaLinks { get; set; }
}

public interface IEventDetail
{
    string EventOrFactClassification { get; }
    string DateValue { get; }
    PlaceStructure PlaceStructure { get; }
    AddressStructure AddressStructure { get; }
    string ResponsibleAgency { get; }
    string ReligiousAffiliation { get; }
    string CauseOfEvent { get; }
    string RestrictionNotice { get; }
    List<NoteStructure> NoteStructures { get; }
    List<SourceCitation> SourceCitations { get; }
    List<MultimediaLink> MultimediaLinks { get; }
}

#region EVENT_DETAIL p. 34
/* 

EVENT_DETAIL:=

n TYPE <EVENT_OR_FACT_CLASSIFICATION> {0:1} p.49
n DATE <DATE_VALUE> {0:1} p.47, 46
n <<PLACE_STRUCTURE>> {0:1} p.38
n <<ADDRESS_STRUCTURE>> {0:1} p.31
n AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
n RELI <RELIGIOUS_AFFILIATION> {0:1} p.60
n CAUS <CAUSE_OF_EVENT> {0:1} p.43
n RESN <RESTRICTION_NOTICE> {0:1} p.60
n <<NOTE_STRUCTURE>> {0:M} p.37
n <<SOURCE_CITATION>> {0:M} p.39
n <<MULTIMEDIA_LINK>> {0:M} p.37, 26

*/
#endregion