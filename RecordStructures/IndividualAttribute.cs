namespace Gedcom.RecordStructures;

public class IndividualAttribute : RecordStructureBase
{
    public IndividualAttribute() : base() { }
    public IndividualAttribute(Record record) : base(record) { }
    public IndividualEventDetail IndividualEventDetail => new IndividualEventDetail(FirstOrDefault(C.EVEN));
}

#region INDIVIDUAL_ATTRIBUTE p. 33
/* 

n TAG <Value_Description> {1:1} p.58
    +1 [CONC | CONT ] <PHYSICAL_DESCRIPTION> {0:M} p.58
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34

*/
#endregion