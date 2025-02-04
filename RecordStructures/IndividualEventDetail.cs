namespace Gedcom.RecordStructure;

public class IndividualEventDetail : RecordStructureBase
{
    public IndividualEventDetail() : base() { }
    public IndividualEventDetail(Record record) : base(record) { }

    public string Age => V(C.AGE);
    public EventDetail EventDetail => new EventDetail(FirstOrDefault(C.EVEN));
}

#region INDIVIDUAL_EVENT_DETAIL p. 34
/* 
https://gedcom.io/specifications/ged551.pdf

INDIVIDUAL_EVENT_DETAIL:=

n <<EVENT_DETAIL>> {1:1} p.32
n AGE <AGE_AT_EVENT> {0:1} p.42

*/
#endregion