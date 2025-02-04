namespace Gedcom.RecordStructure;

public class IndividualAttribute : RecordStructureBase
{
    public IndividualAttribute() : base() { }
    public IndividualAttribute(Record record) : base(record) { }
    public EventDetail EventDetail => new EventDetail(FirstOrDefault(C.EVEN));
}

#region INDIVIDUAL_ATTRIBUTE p. 33
/* 
https://gedcom.io/specifications/ged551.pdf

n TAG <Value_Description> {1:1} p.58
    +1 [CONC | CONT ] <PHYSICAL_DESCRIPTION> {0:M} p.58
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34

*/
#endregion