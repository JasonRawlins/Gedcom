namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class RepositoryRecord : RecordStructureBase, IAddressStructure
{
    public RepositoryRecord() : base() { }
    public RepositoryRecord(Record record) : base(record) { }

    private List<string>? _addressEmails = null;
    public List<string> AddressEmails => _addressEmails ??= ListValues(Tag.Email);

    private List<string>? _addressFaxNumbers = null;
    public List<string> AddressFaxNumbers => _addressFaxNumbers ??= ListValues(Tag.Facimilie);

    private AddressStructure? _addressStructure = null;
    public AddressStructure AddressStructure => _addressStructure ??= First<AddressStructure>(Tag.Address);

    private List<string>? _addressWebPages = null;
    public List<string> AddressWebPages => _addressWebPages ??= ListValues(Tag.Web);

    private string? _automatedRecordId = null;
    public string AutomatedRecordId => _automatedRecordId ??= GetValue(Tag.RecordIdNumber);

    private CallNumber? _callNumber = null;
    public CallNumber CallNumber => _callNumber ??= First<CallNumber>(Tag.CallNumber);

    private ChangeDate? _changeDate = null;
    public ChangeDate ChangeDate => _changeDate ??= First<ChangeDate>(Tag.Change);

    private string? _name = null;
    public string Name => _name ??= GetValue(Tag.Name);

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private List<string>? _phoneNumbers = null;
    public List<string> PhoneNumbers => _phoneNumbers ??= ListValues(Tag.Phone);

    private UserReferenceNumber? _userReferenceNumber = null;
    public UserReferenceNumber UserReferenceNumber => _userReferenceNumber ??= First<UserReferenceNumber>(Tag.Reference);
    
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {Name}";
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