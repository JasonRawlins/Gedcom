using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class MultimediaFormatDto(MultimediaFormat multimediaFormat) : GedcomDto
{
    public string? SourceMediaType { get; set; } = GetString(multimediaFormat.SourceMediaType);
    public override string ToString() => $"{SourceMediaType}";
}