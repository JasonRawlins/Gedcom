namespace Gedcom.RecordStructures;

public class NoteRecord : RecordStructureBase
{
    public NoteRecord() : base() { }
    public NoteRecord(Record record) : base(record) { }

    public UserReferenceNumber UserReferenceNumber => FirstOrDefault<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public ChangeDate ChangeDate => FirstOrDefault<ChangeDate>(C.CHAN);
}

#region NOTE_RECORD (NOTE) p. 27
/* 

NOTE_RECORD:=

n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT> {1:1} p.63
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<CHANGE_DATE>> {0:1} p.31

*/
#endregion