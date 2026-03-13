using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class MultimediaLinkDto(MultimediaLink multimediaLink) : GedcomDto
{
    public string? DescriptiveTitle { get; set; } = GetString(multimediaLink.DescriptiveTitle);
    public List<MultimediaFileReferenceNumberDto>? MultimediaFileReferenceNumbers { get; set; } = GetList(multimediaLink.MultimediaFileReferenceNumbers.Select(mfrn => new MultimediaFileReferenceNumberDto(mfrn)).ToList());
    public string? SourceMediaType { get; set; } = GetString(multimediaLink.SourceMediaType);
    public string? Xref { get; set; } = multimediaLink.Xref;
    public override string ToString() => $"{DescriptiveTitle}";
}