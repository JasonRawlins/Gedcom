namespace Gedcom;

public class Gedcom
{
    private readonly List<List<GedcomLine>> Level0Records = [];
    private readonly List<Record> Records = [];
    public Gedcom(List<GedcomLine> gedcomLines)
    {
        Level0Records = GetGedcomLinesForLevel(0, gedcomLines);

        foreach (var level0Record in Level0Records)
        {
            Records.Add(new Record(level0Record));
        }

        // foreach level record
    }

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

        var lastGedcomLine = gedcomLines[gedcomLines.Count - 1];
        gedcomLinesAtThisLevel.Add([lastGedcomLine]);

        return gedcomLinesAtThisLevel.Skip(1).ToList();
    }
}

