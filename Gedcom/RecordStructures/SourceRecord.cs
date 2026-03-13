namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class SourceRecord : RecordStructureBase
{
    public SourceRecord() : base() { }
    public SourceRecord(Record record) : base(record) { }

    private string? _automatedRecordId = null;
    public string AutomatedRecordId => _automatedRecordId ??= GetValue(Tag.RecordIdNumber);

    private string? _callNumber = null;
    public string CallNumber => _callNumber ??= GetValue(Tag.CallNumber);

    private ChangeDate? _changeDate = null;
    public ChangeDate ChangeDate => _changeDate ??= First<ChangeDate>(Tag.Change);

    private List<MultimediaLink>? _multimediaLinks = null;
    public List<MultimediaLink> MultimediaLinks => _multimediaLinks ??= List<MultimediaLink>(Tag.Object);

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? _repositoryXref = null;
    public string RepositoryXref => _repositoryXref ??= GetValue(Tag.Repository);

    private NoteStructure? _sourceDescriptiveTitle = null;
    public NoteStructure SourceDescriptiveTitle => _sourceDescriptiveTitle ??= First<NoteStructure>(Tag.Title);

    private NoteStructure? _sourceFiledByEntry = null;
    public NoteStructure SourceFiledByEntry => _sourceFiledByEntry ??= First<NoteStructure>(Tag.Abbreviation);

    private NoteStructure? _sourceOriginator = null;
    public NoteStructure SourceOriginator => _sourceOriginator ??= First<NoteStructure>(Tag.Author);

    private NoteStructure? _sourcePublicationFacts = null;
    public NoteStructure SourcePublicationFacts => _sourcePublicationFacts ??= First<NoteStructure>(Tag.Publication);

    private SourceRecordData? _sourceRecordData = null;
    public SourceRecordData SourceRecordData => _sourceRecordData ??= First<SourceRecordData>(Tag.Data);

    private List<SourceRepositoryCitation>? _sourceRepositoryCitations = null;
    public List<SourceRepositoryCitation> SourceRepositoryCitations => _sourceRepositoryCitations ??= List<SourceRepositoryCitation>(Tag.Repository);

    private NoteStructure? _textFromSource = null;
    public NoteStructure TextFromSource => _textFromSource ??= First<NoteStructure>(Tag.Text);

    private List<UserReferenceNumber>? _userReferenceNumbers = null;
    public List<UserReferenceNumber> UserReferenceNumbers => _userReferenceNumbers ??= List<UserReferenceNumber>(Tag.Reference);
    
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {AutomatedRecordId}";
}

#region SOURCE_RECORD p. 27-28
/* 

SOURCE_RECORD:=

0 @<XREF:SOUR>@ SOUR {1:1}
    1 DATA {0:1}
        2 EVEN <EVENTS_RECORDED> {0:M} p.50
            3 DATE <DATE_PERIOD> {0:1} p.46
            3 PLAC <SOURCE_JURISDICTION_PLACE> {0:1} p.62
        2 AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
        2 <<NOTE_STRUCTURE>> {0:M} p.37
    1 AUTH <SOURCE_ORIGINATOR> {0:1} p.62
        2 [CONC|CONT] <SOURCE_ORIGINATOR> {0:M} p.62
    1 TITL <SOURCE_DESCRIPTIVE_TITLE> {0:1} p.62
        2 [CONC|CONT] <SOURCE_DESCRIPTIVE_TITLE> {0:M} p.62
    1 ABBR <SOURCE_FILED_BY_ENTRY> {0:1} p.62
    1 PUBL <SOURCE_PUBLICATION_FACTS> {0:1} p.62
        2 [CONC|CONT] <SOURCE_PUBLICATION_FACTS> {0:M} p.62
    1 TEXT <TEXT_FROM_SOURCE> {0:1} p.63
        2 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M} p.63
    1 <<SOURCE_REPOSITORY_CITATION>> {0:M} p.40
    1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    1 <<CHANGE_DATE>> {0:1} p.31
    1 <<NOTE_STRUCTURE>> {0:M} p.37
    1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26

Source records are used to provide a bibliographic description of the source cited. (See the
<<SOURCE_CITATION>> structure, page 39, which contains the pointer to this source record.)

*/
#endregion