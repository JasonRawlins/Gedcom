using Gedcom.Core;

namespace Gedcom.RecordStructures;

public class MultimediaFormat : RecordStructureBase
{
    public MultimediaFormat() : base() { }
    public MultimediaFormat(Record record) : base(record) { }

    public string SourceMediaType => _(C.MEDI);
}

#region MULTIMEDIA_FORMAT p. 54
/* 

n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62

*/
#endregion