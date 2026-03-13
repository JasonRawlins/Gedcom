using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class MultimediaFileReferenceNumberDto(MultimediaFileReferenceNumber multimediaFileReferenceNumber) : GedcomDto
{
    public MultimediaFormatDto? MultimediaFormat { get; set; } = GetRecord(new MultimediaFormatDto(multimediaFileReferenceNumber.MultimediaFormat));
    public override string ToString() => $"{MultimediaFormat?.SourceMediaType}";
}