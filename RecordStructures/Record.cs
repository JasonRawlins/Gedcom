using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

public class Record
{
    public int Level { get; }
    public string Tag { get; } = "";
    public string Value { get; } = "";
    [JsonIgnore]
    public List<GedcomLine> GedcomLines { get; } = [];
    public List<Record> Records { get; } = [];

    public Record? this[string tag] => Records.FirstOrDefault(r => r.Tag.Equals(tag));

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

    public static Record Default
    {
        get
        {
            var gedcomLines = new List<GedcomLine>
            {
                new GedcomLine
                {
                    Level = -1,
                    Tag = C.EMPTY,
                    Value = ""
                }
            };

           return new Record(gedcomLines);
        }
    }

    public override string ToString() => $"{Level} {Tag} {Value}";
}
