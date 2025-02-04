namespace Gedcom.RecordStructure;

public class ChangeDate : RecordStructureBase
{
    public ChangeDate() { }
    public ChangeDate(Record record) : base(record) { }

    public string Date => V(C.DATE);
    public string Time => V(C.TIME);

    public NoteStructure? Note
    {
        get
        {
            var timeRecord = Record.Records.FirstOrDefault(r => r.Tag.Equals(C.NOTE));

            if (timeRecord != null) 
            {
                return new NoteStructure(timeRecord);
            }

            return null;
        }
    }
}

#region CHANGE_DATE (CHAN) p. 31
/* 
https://gedcom.io/specifications/ged551.pdf

CHANGE_DATE:=

n CHAN {1:1}
    +1 DATE <CHANGE_DATE> {1:1} p.44
        +2 TIME <TIME_VALUE> {0:1} p.63
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    
    The change date is intended to only record the last change to a record. Some systems may want to
    manage the change process with more detail, but it is sufficient for GEDCOM purposes to indicate
    the last time that a record was modified.
*/
#endregion