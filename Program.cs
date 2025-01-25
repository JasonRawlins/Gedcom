using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Gedcom;

public class Program
{
    static void Main(string[] args)
    {
        var gedcomFile = File.ReadAllLines("***REMOVED***");
        var gedcomLines = gedcomFile.Select(gl => ParseLine(gl)).ToList();
        var gedcom = new Gedcom(gedcomLines);

        var jed = gedcom.GetINDI("@***REMOVED***@");
        var jlr = gedcom.GetINDI("@***REMOVED***@");

        var jlr_jed = gedcom.GetFAM("@F3@");

        foreach (var partner in jlr_jed.Partners)
        {
            var person = gedcom.GetINDI(partner.Value);
            Console.WriteLine(person);
        }
     
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