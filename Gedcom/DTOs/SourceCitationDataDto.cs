using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class SourceCitationDataDto(SourceCitationData sourceCitationData) : GedcomDto
{
    public string? EntryRecordingDate { get; set; } = GetString(sourceCitationData.EntryRecordingDate);
    public List<string> TextFromSources { get; set; } = [.. sourceCitationData.TextFromSources.Select(t => t.Text)];

    public override string ToString() => $"Count: {TextFromSources.Count}";
}