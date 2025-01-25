namespace Gedcom;

public class Gedcom
{
    private List<List<GedcomLine>> Level0Records { get; } = [];
    public List<Record> Records { get; } = [];
    public Gedcom(List<GedcomLine> gedcomLines)
    {
        Level0Records = GetGedcomLinesForLevel(0, gedcomLines);

        foreach (var level0Record in Level0Records)
        {
            Records.Add(new Record(level0Record));
        }
    }

    public INDI GetINDI(string extId) => new INDI(Records.First(r => r.Tag.Equals(Tag.INDI) && r.Value.Equals(extId)));
    public FAM GetFAM(string famId) => new FAM(Records.First(r => r.Tag.Equals(Tag.FAM) && r.Value.Equals(famId)));

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

