namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class UserReferenceNumber : RecordStructureBase
{
    public UserReferenceNumber() { }
    public UserReferenceNumber(Record record) : base(record) { }

    private string? _userReferenceType = null;
    public string UserReferenceType => _userReferenceType ??= GetValue(Tag.Type);

    public override string ToString() => $"{Record.Value}, {UserReferenceType}";
}

#region USER_REFERENCE_TYPE (REFN) p. 27
/* 

n @
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64

*/
#endregion