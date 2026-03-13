namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class HeaderData : RecordStructureBase
{
    public HeaderData() : base() { }
    public HeaderData(Record record) : base(record) { }


    private NoteStructure? _copyrightSourceData = null;
    public NoteStructure CopyrightSourceData => _copyrightSourceData ??= First<NoteStructure>(Tag.Copyright);

    private string? _publicationDate = null;
    public string PublicationDate => _publicationDate ??= GetValue(Tag.Date);

    public override string ToString() => $"{Record.Value}, {PublicationDate}";
}

#region HeaderSOUR p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 DATA <NAME_OF_SOURCE_DATA> {0:1} p.54
            +3 DATE <PUBLICATION_DATE> {0:1) p.59
            +3 COPR <COPYRIGHT_SOURCE_DATA> {0:1) p.44
                +4 [CONT|CONC]<COPYRIGHT_SOURCE_DATA> {0

*/
#endregion