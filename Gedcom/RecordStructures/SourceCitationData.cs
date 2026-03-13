namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class SourceCitationData : RecordStructureBase
{
    public SourceCitationData() : base() { }
    public SourceCitationData(Record record) : base(record) { }

    private string? _entryRecordingDate = null;
    public string EntryRecordingDate => _entryRecordingDate ??= GetValue(Tag.Date);

    private List<NoteStructure>? _textFromSources = null;
    public List<NoteStructure> TextFromSources => _textFromSources ??= List<NoteStructure>(Tag.Text);

    public override string ToString() => $"{Record.Value}, {EntryRecordingDate}";
}

#region SOUR.DATA p. 39
/* 

n SOUR @<XREF:SOUR>@ {1:1} p.27
    +1 DATA {0:1}
        +2 DATE <ENTRY_RECORDING_DATE> {0:1} p.48
        +2 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
            +3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}

*/
#endregion