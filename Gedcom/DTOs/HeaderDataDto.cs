using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class HeaderDataDto(HeaderData headerData) : GedcomDto
{
    public NoteDto? CopyrightSourceData { get; set; } = GetRecord(new NoteDto(headerData.CopyrightSourceData));
    public string? PublicationDate { get; set; } = GetString(headerData.PublicationDate);
    public override string ToString() => $"{PublicationDate}";
}