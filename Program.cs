using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Gedcom;

public class Program
{
    static void Main(string[] args)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyDirectoryName = Path.GetDirectoryName(assembly.Location) ?? "";
        var treeName = "Developer Tree";
        var gedFullName = Path.Combine(assemblyDirectoryName, $"{treeName}.ged");
        var jsonFullName = Path.Combine(assemblyDirectoryName, $"{treeName}.json");

        if (!File.Exists(gedFullName))
        {
            Console.WriteLine($"Could not find the file '{treeName}.ged'");
            return;
        }

        var gedcomFile = File.ReadAllLines(gedFullName);
        var gedcomLines = gedcomFile.Select(ParseLine).ToList();
        var gedcom = new Gedcom(gedcomLines);

        var jsonText = JsonSerializer.Serialize(
            gedcom, new JsonSerializerOptions() 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

        File.WriteAllText(Path.Combine(assemblyDirectoryName, jsonFullName), jsonText);
        Console.WriteLine(jsonText);
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