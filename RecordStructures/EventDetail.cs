namespace Gedcom.RecordStructures;

public class EventDetail : RecordStructureBase
{
    public EventDetail() : base() { }
    public EventDetail(Record record) : base(record) { }

    public string EventOrFactClassification => V(C.TYPE);
    public string DateValue => V(C.DATE);
    public PlaceStructure PlaceStructure => FirstOrDefault<PlaceStructure>(C.PLAC);
    public AddressStructure AddressStructure => FirstOrDefault<AddressStructure>(C.ADDR);
    public string ResponsibleAgency => V(C.AGNC);
    public string ReligiousAffiliation => V(C.RELI);
    public string CauseOfEvent => V(C.CAUS);
    public string RestrictionNotice => V(C.RESN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
    public List<MultiMediaLink> MultiMediaLinks => List<MultiMediaLink>(C.OBJE);
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