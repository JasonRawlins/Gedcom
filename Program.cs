using Gedcom;
using System.Reflection;

public class Program
{
    static void Main(string[] args)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyDirectoryName = @"c:\temp\Gedcom.NET"; // Path.GetDirectoryName(assembly.Location) ?? "";
        var treeName = "DeveloperTree";
        var gedFullName = Path.Combine(assemblyDirectoryName, "Resources", $"{treeName}.ged");
        var jsonFullName = Path.Combine(assemblyDirectoryName, "Resources", $"{treeName}.json");

        if (!File.Exists(gedFullName))
        {
            Console.WriteLine($"Could not find the file '{gedFullName}'");
            return;
        }

        var gedFileLines = File.ReadAllLines(gedFullName);
        var gedcomLines = gedFileLines.Select(GedcomLine.ParseLine).ToList();
        var gedcom = new Gedcom.Gedcom(gedcomLines);
        

        var individualRecord = gedcom.GetIndividualRecord("@***REMOVED***@"); // 
        //var jsonText = JsonSerializer.Serialize(individualRecord, JsonSerializerOptions);
        //File.WriteAllText(jsonFullName, jsonText);
    }
}