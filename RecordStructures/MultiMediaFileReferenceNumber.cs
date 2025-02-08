namespace Gedcom.RecordStructures;

public class MultimediaFileReferenceNumber : RecordStructureBase
{
    public MultimediaFileReferenceNumber() { }
    public MultimediaFileReferenceNumber(Record record) : base(record) { }

    public MultiMediaFormat MultiMediaFormat => new MultiMediaFormat(FirstOrDefault(C.FORM));
}

#region STRUCTURE_NAME p. 37
/* 

n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54

*/
#endregion