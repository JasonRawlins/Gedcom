namespace Gedcom.RecordStructure;

public class NoteRecord : RecordStructureBase
{
    public NoteRecord(Record record) : base(record) { }

    public ChangeDate ChangeDate => List<ChangeDate>(C.CHAN).First();
    public UserReferenceNumber UserReferenceNumber => new UserReferenceNumber(FirstOrDefault(C.REFN));
}

#region STRUCTURE_NAME (NOTE) p. 27
/* 

NOTE_RECORD:=

n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT> {1:1} p.63
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<CHANGE_DATE>> {0:1} p.31

*/
#endregion