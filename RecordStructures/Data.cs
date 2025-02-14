namespace Gedcom.RecordStructures;

public class Data : RecordStructureBase
{
    public Data() : base() { }
    public Data(Record record) : base(record) { }

    public string EntryRecordingDate => _(C.DATE);
}

#region STRUCTURE_NAME (DATA) p. 39
/* 

n SOUR @<XREF:SOUR>@ {1:1} p.27
    +1 DATA {0:1}
        +2 DATE <ENTRY_RECORDING_DATE> {0:1} p.48
        +2 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
            +3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}

*/
#endregion