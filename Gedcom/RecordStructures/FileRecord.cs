namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class FileRecord : RecordStructureBase
{
    public FileRecord() : base() { }
    public FileRecord(Record record) : base(record) { }


    private FormRecord? _formRecord = null;
    public FormRecord FormRecord => _formRecord ??= First<FormRecord>(Tag.Format);

    private string? _title = null;
    public string Title => _title ??= GetValue(Tag.Title);

    public override string ToString() => $"{Title}";
}

#region MULTIMEDIA_FILE_REFN p. 26
/* 

n @XREF:OBJE@ OBJE {1:1}
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54

*/
#endregion