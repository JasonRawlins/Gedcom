using Gedcom.Core;

namespace Gedcom.RecordStructures;

public class RepositoryRecord : RecordStructureBase, IAddressStructure
{
    internal RepositoryRecord() : base() { }
    public RepositoryRecord(Record record) : base(record) { }
    public string Name => _(C.NAME);
    public AddressStructure AddressStructure => First<AddressStructure>(C.ADDR);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
    public CallNumber CallNumber => First<CallNumber>(C.CALN);

    #region IAddressStructure
    public List<string> PhoneNumber => ListValues(C.PHON);
    public List<string> AddressEmail => ListValues(C.EMAIL);
    public List<string> AddressFax => ListValues(C.FAX);
    public List<string> AddressWebPage => ListValues(C.WWW);
    #endregion
}

#region REPOSITORY_RECORD p. 27
/* 

REPOSITORY_RECORD:=

n @<XREF:REPO>@ REPO {1:1}
    +1 NAME <NAME_OF_REPOSITORY> {1:1} p.54
    +1 <<ADDRESS_STRUCTURE>> {0:1} p.31
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<CHANGE_DATE>> {0:1} p.31
    +1 CALN <SOURCE_CALL_NUMBER> {0:M} p.61
        +2 MEDI <SOURCE_MEDIA_TYPE> {0:1}

*/
#endregion