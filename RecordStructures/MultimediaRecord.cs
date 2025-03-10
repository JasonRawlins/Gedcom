namespace Gedcom.RecordStructures;

public class MultimediaRecord : RecordStructureBase
{
    public MultimediaRecord() : base() { }
    public MultimediaRecord(Record record) : base(record) { }

    public List<string> MultimediaFileReferenceNumbers => List(r => r.Tag.Equals(C.FILE)).Select(r => r.Value).ToList();
    public MultimediaFormat MultimediaFormat => First<MultimediaFormat>(C.FORM);
    public string DescriptiveTitle => _(C.TITL);
    public UserReferenceNumber UserReferenceNumber => First<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
}

#region MULTIMEDIA_RECORD p. 26
/* 

n @XREF:OBJE@ OBJE {1:1}
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 TYPE <SOURCE_MEDIA_TYPE> {0:1} p.62
        +2 TITL <DESCRIPTIVE_TITLE> {0:1} p.48
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<CHANGE_DATE>> {0:1} p.31

The BLOB context of the multimedia record was removed in version 5.5.1. A reference to a multimedia
file was added to the record structure. The file reference occurs one to many times so that multiple files
can be grouped together, each pertaining to the same context. For example, if you wanted to associate a
sound clip and a photo, you would reference each multimedia file and indicate the format using the
FORM tag subordinate to each file reference.

*/
#endregion