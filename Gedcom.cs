namespace Gedcom;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tags;

[JsonConverter(typeof(GedcomJsonConverter))]
public class Gedcom
{
    public List<Record> Records { get; } = [];
    public Gedcom(List<GedcomLine> gedcomLines)
    {
        foreach (var level0Record in GetGedcomLinesForLevel(0, gedcomLines))
        {
            Records.Add(new Record(level0Record));
        }
    }

    public FAM GetFAM(string xrefFAM) => new(Records.First(r => r.Tag.Equals(T.FAM) && r.Value.Equals(xrefFAM)));
    public List<FAM> GetFAMs() => Records.Where(r => r.Tag.Equals(T.FAM)).Select(r => new FAM(r)).ToList();
    public INDI GetINDI(string xrefINDI) => new(Records.First(r => r.Tag.Equals(T.INDI) && r.Value.Equals(xrefINDI)));
    public List<INDI> GetINDIs() => Records.Where(r => r.Tag.Equals(T.INDI)).Select(r => new INDI(r)).ToList();
    public SOUR GetSOUR(string xrefSOUR) => new(Records.First(r => r.Tag.Equals(T.SOUR) && r.Value.Equals(xrefSOUR)));
    public List<SOUR> GetSOURs() => Records.Where(r => r.Tag.Equals(T.SOUR)).Select(r => new SOUR(r)).ToList();
    public static List<List<GedcomLine>> GetGedcomLinesForLevel(int level, List<GedcomLine> gedcomLines)
    {
        var gedcomLinesAtThisLevel = new List<List<GedcomLine>>();
        var currentGedcomLines = new List<GedcomLine>();

        foreach (var gedcomLine in gedcomLines)
        {
            if (gedcomLine.Level == level)
            {
                gedcomLinesAtThisLevel.Add(currentGedcomLines);
                currentGedcomLines = [gedcomLine];                
            }
            else
            {
                currentGedcomLines.Add(gedcomLine);
            }
        }

        gedcomLinesAtThisLevel.Add(currentGedcomLines);

        return gedcomLinesAtThisLevel.Skip(1).ToList();
    }
    public string Value(Record? record) => record?.Value ?? "";

    public Record CORP => SOUR.Records.First(r => r.Tag.Equals(T.CORP));
    public Record HEAD => Records.First(r => r.Tag.Equals(T.HEAD));
    public Record RIN => _TREE.Records.First(r => r.Tag.Equals(T.RIN));
    public Record SOUR => HEAD.Records.First(r => r.Tag.Equals(T.SOUR));
    public Record _TREE => SOUR.Records.First(r => r.Tag.Equals(_TREE.Tag));

    public override string ToString() => $"{_TREE.Value} ({RIN.Value})";

    public string ToGed() 
    {
        var gedStringBuilder = new StringBuilder();
        foreach (var record in Records)
        {
            gedStringBuilder.Append(GetGedcomLinesText(record));
        }

        return gedStringBuilder.ToString();
    }

    private string GetGedcomLinesText(Record record)
    {
        var gedcomLinesStringBuilder = new StringBuilder();
        gedcomLinesStringBuilder.AppendLine(new String(' ', record.Level * 1) + record);

        foreach (var recursiveRecord in record.Records)
        {
            gedcomLinesStringBuilder.Append(GetGedcomLinesText(recursiveRecord));
        }

        return gedcomLinesStringBuilder.ToString();
    }

    public static Gedcom ParseJson(string json)
    {
        return new Gedcom(new List<GedcomLine>());
    }
}

public class GedcomJsonConverter : JsonConverter<Gedcom>
{
    public override Gedcom? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Gedcom.ParseJson(reader.GetString());
    public override void Write(Utf8JsonWriter writer, Gedcom gedcom, JsonSerializerOptions options)
    {
        var gedcomJsonObject = new
        {
            Head = new
            {
                Subm = Value(gedcom.HEAD[T.SUBM]),
                Sour = new
                {
                    Name = Value(gedcom.HEAD[T.SOUR]?[T.NAME]),
                    Vers = Value(gedcom.HEAD[T.VERS]),
                    _Tree = new
                    {
                        rin = Value(gedcom.HEAD[T.RIN]),
                        _env = Value(gedcom.HEAD[T._ENV]),
                    },
                    Corp = new
                    {
                        Value = gedcom.CORP.Value,
                        Phon = Value(gedcom.CORP[T.PHON]),
                        Www = Value(gedcom.CORP[T.WWW]),
                        Addr = Value(gedcom.CORP[T.ADDR])
                    }
                },
                Date = Value(gedcom.HEAD[T.DATE]),
                Gedc = Value(gedcom.HEAD[T.GEDC]),
                Char_ = Value(gedcom.HEAD[T.CHAR])
            }
        };

        var jsonText = JsonSerializer.Serialize(gedcomJsonObject,
            new JsonSerializerOptions() { WriteIndented = true });
        writer.WriteStringValue(jsonText);

        File.WriteAllText(@"c:\temp\gedcom.json", jsonText);
    }

    private string Value(Record? record) => record?.Value ?? "";
}
