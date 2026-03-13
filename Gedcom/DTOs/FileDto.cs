using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class FileDto(FileRecord fileRecord) : GedcomDto
{
    public FormDto? Form { get; set; } = GetRecord(new FormDto(fileRecord.FormRecord));
    public string? Title { get; set; } = GetString(fileRecord.Title);
    public override string ToString() => $"{Title}";
}