using Gedcom.Tags;
using System.Text.Json.Serialization;

namespace Gedcom;

public class Record
{
    //[JsonIgnore]
    public int Level { get; }
    public string Tag { get; } = "";
    public string Value { get; } = "";
    [JsonIgnore]
    public List<GedcomLine> GedcomLines { get; } = [];
    public List<Record> Records { get; } = [];

    public Record? this[string tag]
    {
        get => Records.FirstOrDefault(r => r.Tag.Equals(tag));
    }

    public Record(List<GedcomLine> gedcomLines)
    {
        GedcomLines = gedcomLines;
        Level = gedcomLines[0].Level;
        Tag = gedcomLines[0].Tag;
        Value = gedcomLines[0].Value;

        if (gedcomLines.Count > 1)
        {
            var nextLevelGedcomLines = Gedcom.GetGedcomLinesForLevel(Level + 1, gedcomLines.Skip(1).ToList());
            foreach (var nextLevelGedcomLine in nextLevelGedcomLines)
            {
                Records.Add(new Record(nextLevelGedcomLine));
            }            
        }
    }

    public override string ToString() => $"{Level} {Tag} {Value}";
}
