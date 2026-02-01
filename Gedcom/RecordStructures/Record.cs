using Gedcom.Core;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

public class Record
{
    public int Level { get; }
    public string Tag { get; } = "";
    public string Value { get; } = "";
    [JsonIgnore]
    public List<GedcomLine> GedcomLines { get; } = []; // The Gedcom lines of this record and all its child records.
    public List<Record> Records { get; } = []; // A collection of all parsed child records. 	
    internal bool IsEmpty => Level == -1 && Tag.Equals(Constants.Empty) && Records.Count == 0;

    public Record(List<GedcomLine> gedcomLines)
    {
        GedcomLines = gedcomLines;
        Level = gedcomLines[0].Level;
        Tag = gedcomLines[0].Tag;
        Value = gedcomLines[0].Value;

        if (gedcomLines.Count > 1)
        {
            var nextLevelGedcomLines = Core.Gedcom.GetGedcomLinesForLevel(Level + 1, [.. gedcomLines.Skip(1)]);
            foreach (var nextLevelGedcomLine in nextLevelGedcomLines)
            {
                Records.Add(new Record(nextLevelGedcomLine));
            }
        }
    }

    public bool IsQueryMatch(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return true;
        }

        if (Value.Contains(query, StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }

        foreach (var record in Records)
        {
            if (record.IsQueryMatch(query))
            {
                return true;
            }
        }

        return false;
    }

    // Gedcom.NET does not deal with null records. Instead, it uses an empty record.  
    public static Record Empty
    {
        get
        {
            var gedcomLines = new List<GedcomLine>
            {
                new() {
                    Level = -1,
                    Tag = Constants.Empty,
                    Value = ""
                }
            };

           return new Record(gedcomLines);
        }
    }

    public override string ToString() => $"{Level} {Tag} {Value}";
}
