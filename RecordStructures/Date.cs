namespace Gedcom.RecordStructures;

public class Date : RecordStructureBase
{
    public Date() : base() { }
    public Date(Record record) : base(record) { }

    public string TimeValue => V(C.TIME);
}

#region DATE p. 45
/* 

+1 DATE <TRANSMISSION_DATE> {0:1} p.63
    +2 TIME <TIME_VALUE> {0:1} p.63

*/
#endregion