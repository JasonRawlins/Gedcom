//using Newtonsoft.Json;

//namespace Gedcom.RecordStructures;

//// The Gedcom Standard 5.5.1 documentation is at the end of this file.
//[JsonConverter(typeof(FamilyEventDetailJsonConverter))]
//public class EventDetail : RecordStructureBase, IEventDetail
//{
//    public EventDetail() : base() { }
//    public EventDetail(Record record) : base(record) { }

//    public AddressStructure AddressStructure => First<AddressStructure>(Tag.Address);
//    public string CauseOfEvent => GetValue(Tag.Cause);
//    public string DateValue => GetValue(Tag.Date);
//    public string EventOrFactClassification => GetValue(Tag.Type);
//    public GedcomDate GedcomDate => GedcomDate.Parse(DateValue);
//    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(Tag.Object);
//    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
//    public PlaceStructure PlaceStructure => First<PlaceStructure>(Tag.Place);
//    public string ReligiousAffiliation => GetValue(Tag.Religion);
//    public string ResponsibleAgency => GetValue(Tag.Agency);
//    public string RestrictionNotice => GetValue(Tag.Restriction);
//    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);

//    public override string ToString() => $"{Record.Value}, {GedcomDate.DayMonthYear}";
//}

//internal class EventDetailJsonConverter : JsonConverter<EventDetail>
//{
//    public override EventDetail? ReadJson(JsonReader reader, Type objectType, EventDetail? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

//    public override void WriteJson(JsonWriter writer, EventDetail? eventDetail, JsonSerializer serializer)
//    {
//        if (eventDetail == null) throw new ArgumentNullException(nameof(eventDetail));

//        serializer.Serialize(writer, new EventDetailJson(eventDetail));
//    }
//}

//internal class EventDetailJson : GedcomJson
//{
//    public EventDetailJson(EventDetail eventDetail)
//    {
//        Address = JsonRecord(eventDetail.AddressStructure);
//        CauseOfEvent = JsonString(eventDetail.CauseOfEvent);
//        DateValue = JsonString(eventDetail.DateValue);
//        EventOrFactClassification = JsonString(eventDetail.EventOrFactClassification);
//        GedcomDate = JsonRecord(eventDetail.GedcomDate);
//        MultimediaLinks = JsonList(eventDetail.MultimediaLinks);
//        Notes = JsonList(eventDetail.NoteStructures);
//        Place = JsonRecord(eventDetail.PlaceStructure);
//        ReligiousAffiliation = JsonString(eventDetail.ReligiousAffiliation);
//        ResponsibleAgency = JsonString(eventDetail.ResponsibleAgency);
//        RestrictionNotice = JsonString(eventDetail.RestrictionNotice);
//        SourceCitations = JsonList(eventDetail.SourceCitations);
//    }

//    public AddressStructure? Address { get; set; }
//    public string? CauseOfEvent { get; set; }
//    public string? DateValue { get; set; }
//    public string? EventOrFactClassification { get; set; }
//    public GedcomDate GedcomDate { get; set; }
//    public List<MultimediaLink>? MultimediaLinks { get; set; }
//    public List<NoteStructure>? Notes { get; set; }
//    public PlaceStructure? Place { get; set; }
//    public string? ReligiousAffiliation { get; set; }
//    public string? ResponsibleAgency { get; set; }
//    public string? RestrictionNotice { get; set; }
//    public List<SourceCitation>? SourceCitations { get; set; }
//}

//#region EVENT_DETAIL p. 34
///* 

//EVENT_DETAIL:=

//n TYPE <EVENT_OR_FACT_CLASSIFICATION> {0:1} p.49
//n DATE <DATE_VALUE> {0:1} p.47, 46
//n <<PLACE_STRUCTURE>> {0:1} p.38
//n <<ADDRESS_STRUCTURE>> {0:1} p.31
//n AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
//n RELI <RELIGIOUS_AFFILIATION> {0:1} p.60
//n CAUS <CAUSE_OF_EVENT> {0:1} p.43
//n RESN <RESTRICTION_NOTICE> {0:1} p.60
//n <<NOTE_STRUCTURE>> {0:M} p.37
//n <<SOURCE_CITATION>> {0:M} p.39
//n <<MULTIMEDIA_LINK>> {0:M} p.37, 26

//*/
//#endregion