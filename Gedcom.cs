namespace Gedcom;
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

    public FAM GetFAM(string xrefFAM) => new FAM(Records.First(r => r.Tag.Equals(Tag.FAM) && r.Value.Equals(xrefFAM)));
    public List<FAM> GetFAMs() => Records.Where(r => r.Tag.Equals(Tag.FAM)).Select(r => new FAM(r)).ToList();
    public INDI GetINDI(string xrefINDI) => new INDI(Records.First(r => r.Tag.Equals(Tag.INDI) && r.Value.Equals(xrefINDI)));
    public List<INDI> GetINDIs() => Records.Where(r => r.Tag.Equals(Tag.INDI)).Select(r => new INDI(r)).ToList();
    public SOUR GetSOUR(string xrefSOUR) => new SOUR(Records.First(r => r.Tag.Equals(Tag.SOUR) && r.Value.Equals(xrefSOUR)));
    public List<SOUR> GetSOURs() => Records.Where(r => r.Tag.Equals(Tag.SOUR)).Select(r => new SOUR(r)).ToList();
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
}

