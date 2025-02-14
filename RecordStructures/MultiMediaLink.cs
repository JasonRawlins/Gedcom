namespace Gedcom.RecordStructures;

public class MultiMediaLink : RecordStructureBase
{
    public MultiMediaLink() : base() { }
    public MultiMediaLink(Record record) : base(record) { }

    public List<MultimediaFileReferenceNumber> MultimediaFileReferenceNumbers => List<MultimediaFileReferenceNumber>(C.FILE);
    public string SourceMediaType => _(C.MEDI);
    public string DiscriptiveTitle => _(C.TITL);
}

#region MULTIMEDIA_LINK p. 37
/* 

MULTIMEDIA_LINK: =

n OBJE @<XREF:OBJE>@ {1:1} p.26
|
n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54
            +3 MEDI <SOURCE_MEDIA_TYPE> {0:1} p.62
    +1 TITL <DESCRIPTIVE_TITLE> {0:1} p.48

Note: some systems may have output the following 5.5 structure. The new context above was
introduced in order to allow a grouping of related multimedia files to a particular context.

*/
#endregion