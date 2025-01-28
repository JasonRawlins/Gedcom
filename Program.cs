using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Gedcom;

public class Program
{
    static void Main(string[] args)
    {
        var gedcomFileName = "***REMOVED***";
        var gedcomFile = File.ReadAllLines(gedcomFileName);
        var gedcomLines = gedcomFile.Select(ParseLine).ToList();
        var gedcom = new Gedcom(gedcomLines);

        //gedcom.GetSOURs().ForEach(s => Console.WriteLine($"{s}"));
        //Console.WriteLine(gedcom.ToGed());
        Console.WriteLine(gedcom.ToJson());
        
        // Just for fun, print the gedcom to json.

        Console.ReadLine();
    }

    private static GedcomLine ParseLine(string line)
    {
        var level = int.Parse(line.Substring(0, line.IndexOf(" ")));
        var lineWithoutLevel = line.Substring(line.IndexOf(" ") + 1);
        var secondSpaceIndex = lineWithoutLevel.IndexOf(" ");
        var secondSegment = "";
        var thirdSegment = "";

        if (secondSpaceIndex != -1)
        {
            secondSegment = lineWithoutLevel.Substring(0, lineWithoutLevel.IndexOf(" "));
            thirdSegment = lineWithoutLevel.Substring(lineWithoutLevel.IndexOf(' ') + 1);
        }
        else
        {
            secondSegment = lineWithoutLevel;
        }

        var value = "";
        var tag = "";

        if (level == 0 && Regex.IsMatch(secondSegment, @"@.*@"))
        {
            tag = thirdSegment;
            var extId = Regex.Match(secondSegment, @"@.*@").Value;
            value = extId;
        }
        else
        {
            tag = secondSegment;
            value = thirdSegment;
        }

        return new GedcomLine
        {
            Level = level,
            Tag = tag,
            Value = value
        };
    }
}