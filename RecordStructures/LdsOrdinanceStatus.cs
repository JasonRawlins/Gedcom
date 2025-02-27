namespace Gedcom.RecordStructures;

public class LdsOrdinanceStatus : RecordStructureBase
{
    public string Status => Record.Value;
    public string ChangeDate => _(C.DATE);
}

#region STRUCTURE_NAME p. 
/* 

n SLGS {1:1}
    +1 STAT <LDS_SPOUSE_SEALING_DATE_STATUS> {0:1} p.52
        +2 DATE <CHANGE_DATE> {1:1}

*/
#endregion

