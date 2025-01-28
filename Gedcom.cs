namespace Gedcom;

using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Tags;

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

    public FAM GetFAM(string xrefFAM) => new(Records.First(r => r.Tag.Equals(TagBase.T.FAM) && r.Value.Equals(xrefFAM)));
    public List<FAM> GetFAMs() => Records.Where(r => r.Tag.Equals(TagBase.T.FAM)).Select(r => new FAM(r)).ToList();
    public INDI GetINDI(string xrefINDI) => new(Records.First(r => r.Tag.Equals(TagBase.T.INDI) && r.Value.Equals(xrefINDI)));
    public List<INDI> GetINDIs() => Records.Where(r => r.Tag.Equals(TagBase.T.INDI)).Select(r => new INDI(r)).ToList();
    public SOUR GetSOUR(string xrefSOUR) => new(Records.First(r => r.Tag.Equals(TagBase.T.SOUR) && r.Value.Equals(xrefSOUR)));
    public List<SOUR> GetSOURs() => Records.Where(r => r.Tag.Equals(TagBase.T.SOUR)).Select(r => new SOUR(r)).ToList();
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

    public Record HEAD => Records.First(r => r.Tag.Equals(TagBase.T.HEAD));
    public Record SOUR => HEAD.Records.First(r => r.Tag.Equals(TagBase.T.SOUR));
    public Record _TREE => SOUR.Records.First(r => r.Tag.Equals(_TREE.Tag));
    public Record RIN => _TREE.Records.First(r => r.Tag.Equals(TagBase.T.RIN));

    public override string ToString()
    {
        var _TREE = Records.First(r => r.Tag.Equals(TagBase.T.
            HEAD)).Records.First(r => r.Tag.Equals(TagBase.T.
                SOUR)).Records.First(r => r.Tag.Equals(TagBase.T.
                    _TREE));

        var RIN = _TREE.Records.First(r => r.Tag.Equals(TagBase.T.RIN));

        return $"{_TREE.Value} ({RIN.Value})";

        // 0 HEAD
        //   1 SOUR Ancestry.com Family Trees
        //     2 _TREE _Ancestry Tree Name i.e. "The Davis Family Tree"
        //       3 RIN _NineDigitAncestryRIN i.e. 123456789
    }

    public string ToGed() 
    {
        var gedStringBuilder = new StringBuilder();
        foreach (var record in Records)
        {
            gedStringBuilder.Append(GetGedcomLinesText(record));
        }

        return gedStringBuilder.ToString();
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(Records.Select(r => r).ToList(), new JsonSerializerOptions()
        {
            IgnoreReadOnlyFields = true,
            WriteIndented = true
        });
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
}
