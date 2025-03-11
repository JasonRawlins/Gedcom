using Gedcom.Core;

namespace Gedcom.RecordStructures;

public class IndividualEventDetail : RecordStructureBase
{
    public IndividualEventDetail() : base() { }
    public IndividualEventDetail(Record record) : base(record) { }

    public string AgeAtEvent => _(C.AGE);
}

#region INDIVIDUAL_EVENT_DETAIL p. 34
/* 

INDIVIDUAL_EVENT_DETAIL:=

n <<EVENT_DETAIL>> {1:1} p.32
n AGE <AGE_AT_EVENT> {0:1} p.42

*/
#endregion