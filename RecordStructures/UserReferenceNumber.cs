namespace Gedcom.RecordStructures;

public class UserReferenceNumber : RecordStructureBase
{
    public UserReferenceNumber() { }
    public UserReferenceNumber(Record record) : base(record) { }

    public string UserReferenceType => _(C.TYPE);
}

#region USER_REFERENCE_TYPE (REFN) p. 27
/* 

n @
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64

*/
#endregion