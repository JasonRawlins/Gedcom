namespace Gedcom.RecordStructure;

public class UserReferenceNumber : RecordStructureBase
{
    public UserReferenceNumber() { }
    public UserReferenceNumber(Record record) : base(record) { }

    public string Type => V(C.TYPE);
}

#region USER_REFERENCE_TYPE (REFN) p. 27
/* 
https://gedcom.io/specifications/ged551.pdf

n @
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64

*/
#endregion