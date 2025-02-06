namespace Gedcom.RecordStructures;

public class FamilyEventDetail : RecordStructureBase
{
    public FamilyEventDetail(Record record) : base(record) { }

    public FamilyPartner Husband => new FamilyPartner(FirstOrDefault(C.HUSB));
    public FamilyPartner Wife => new FamilyPartner(FirstOrDefault(C.WIFE));
    public EventDetail EventDetail => new EventDetail(FirstOrDefault(C.EVEN));
}

public class FamilyPartner(Record record)
{
    public string Name => record.Value;
    public string AgeAtEvent => record.Records.FirstOrDefault(r => r.Tag.Equals(C.AGE))?.Value ?? "";
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