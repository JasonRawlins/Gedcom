namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class LdsOrdinanceStatus : RecordStructureBase
{
    private string? _changeDate = null;
    public string ChangeDate => _changeDate ??= GetValue(Tag.Date);

    private string? _status = null;
    public string Status => _status ??= Record.Value;

    public override string ToString() => $"{Record.Value}, {Status}, {ChangeDate}";
}

#region STRUCTURE_NAME p. 
/* 

n SLGS {1:1}
    +1 STAT <LDS_SPOUSE_SEALING_DATE_STATUS> {0:1} p.52
        +2 DATE <CHANGE_DATE> {1:1}

*/
#endregion

