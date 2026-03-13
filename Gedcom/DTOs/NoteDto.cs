using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class NoteDto(NoteStructure noteStructure) : GedcomDto
{
    public string Text { get; set; } = noteStructure.Text;
    private const int TextLengthLimit = 32;
    public override string ToString() => Text.Length <= TextLengthLimit ? Text : Text.Substring(TextLengthLimit);
}