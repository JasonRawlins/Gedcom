namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class MultimediaFileReferenceNumber : RecordStructureBase
{
    public MultimediaFileReferenceNumber() { }
    public MultimediaFileReferenceNumber(Record record) : base(record) { }

    private MultimediaFormat? _multimediaFormat = null;
    public MultimediaFormat MultimediaFormat => _multimediaFormat ??= First<MultimediaFormat>(Tag.Format);

    public override string ToString() => $"{Record.Value}";
}

public class MultimediaFileReferenceNumberDto(MultimediaFileReferenceNumber multimediaFileReferenceNumber) : GedcomDto
{
    public MultimediaFormatDto? MultimediaFormat { get; set; } = GetRecord(new MultimediaFormatDto(multimediaFileReferenceNumber.MultimediaFormat));
    public override string ToString() => $"{MultimediaFormat?.SourceMediaType}";
}

#region STRUCTURE_NAME p. 37
/* 

n OBJE
    +1 FILE <MULTIMEDIA_FILE_REFN> {1:M} p.54
        +2 FORM <MULTIMEDIA_FORMAT> {1:1} p.54

*/
#endregion