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
    internal bool IsEmpty => Level == -1 && Tag.Equals(C.Empty) && Records.Count == 0;

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

    public bool IsQueryMatch(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return true;
        }

        if (Value.ToUpper().Contains(query.ToUpper()))
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

    // Gedcom.NET does not deal with null records. Instead, I've decided to use 
    // an empty record instead of nulls for C# code.  
    public static Record Empty
    {
        get
        {
            var gedcomLines = new List<GedcomLine>
            {
                new GedcomLine
                {
                    Level = -1,
                    Tag = C.Empty,
                    Value = ""
                }
            };

           return new Record(gedcomLines);
        }
    }

    public override string ToString() => $"{Level} {Tag} {Value}";
}
