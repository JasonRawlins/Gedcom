namespace Gedcom.RecordStructures;

public class FamilyEventDetail : RecordStructureBase
{
    public FamilyEventDetail() : base() { }
    public FamilyEventDetail(Record record) : base(record) { }

    public FamilyPartner Husband => First<FamilyPartner>(C.HUSB);
    public FamilyPartner Wife => First<FamilyPartner>(C.WIFE);
    public EventDetail EventDetail => First<EventDetail>(C.EVEN);
}

public class FamilyPartner : RecordStructureBase
{
    public FamilyPartner() { }
    public FamilyPartner(Record record) { }
    public string Name => Record.Value;
    public string AgeAtEvent => Record.Records.FirstOrDefault(r => r.Tag.Equals(C.AGE))?.Value ?? "";
}

#region FAMILY_EVENT_DETAIL p. 32
/* 

FAMILY_EVENT_DETAIL:=

n HUSB {0:1}
    +1 AGE <AGE_AT_EVENT> {1:1} p.42
n WIFE {0:1}
    +1 AGE <AGE_AT_EVENT> {1:1} p.42
n <<EVENT_DETAIL>> {0:1} p.32

*/
#endregion