using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class SourceCitationDataModel
{
    public SourceCitationDataModel(SourceCitationData sourceCitationData)
    {
        EntryRecordingDate = sourceCitationData.EntryRecordingDate;
        TextFromSources = sourceCitationData.TextFromSources.Select(t => t.Text).ToList();
    }

    public string EntryRecordingDate { get; set; }
    public List<string> TextFromSources { get; set; }

    public override string ToString() => $"{EntryRecordingDate}, {TextFromSources.First().Substring(0, 64)}";
}