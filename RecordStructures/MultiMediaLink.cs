namespace Gedcom.RecordStructure;

public class MultiMediaLink : RecordStructureBase
{
    public MultiMediaLink() : base() { }
    public MultiMediaLink(Record record) : base(record) { }

    public string File => V(C.FILE);
    public string Form => V(C.FORM);
    public string SourceMediaType => V(C.MEDI);
    public string Title => V(C.TITL);
}

#region MULTIMEDIA_LINK (OBJE) p. 37
/* 
https://gedcom.io/specifications/ged551.pdf

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