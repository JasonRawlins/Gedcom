using Gedcom.Tags;

namespace Gedcom;

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

    public INDI GetINDI(string extId) => new INDI(Records.First(r => r.Tag.Equals(Tag.INDI) && r.Value.Equals(extId)));
    public List<INDI> GetINDIs() => Records.Where(r => r.Tag.Equals(Tag.INDI)).Select(r => new INDI(r)).ToList();
    public FAM GetFAM(string famId) => new FAM(Records.First(r => r.Tag.Equals(Tag.FAM) && r.Value.Equals(famId)));
    public List<FAM> GetFAMs() => Records.Where(r => r.Tag.Equals(Tag.FAM)).Select(r => new FAM(r)).ToList();
    public SOUR GetSOUR(string extId) => Records.Where(r => r.Tag.Equals(Tag.SOUR)).Select(r => new SOUR(r)).ToList();
    public static List<List<GedcomLine>> GetGedcomLinesForLevel(int level, List<GedcomLine> gedcomLines)
    {
        var gedcomLinesAtThisLevel = new List<List<GedcomLine>>();
        var currentGedcomLines = new List<GedcomLine>();

        for (int i = 0; i < gedcomLines.Count; i++)
        {
            var gedcomLine = gedcomLines[i];

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
}

