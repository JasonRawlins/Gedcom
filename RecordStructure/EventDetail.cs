using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructure;

public class EventDetail : RecordStructureBase
{
    public EventDetail() : base() { }
    public EventDetail(Record record) : base(record) { }

    public string Type => V(C.TYPE);
    public string Date => V(C.DATE);
    // PlaceStructure
    // Address Structure
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
https://gedcom.io/specifications/ged551.pdf

EVENT_DETAIL:=

n <<NOTE_STRUCTURE>> {0:M} p.37
n <<SOURCE_CITATION>> {0:M} p.39
n <<MULTIMEDIA_LINK>> {0:M} p.37, 26

*/
#endregion