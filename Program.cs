using Gedcom;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    static void Main(string[] args)
    {
        GedcomJson.ReturnAllValues = false;
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

        var jsonText = JsonSerializer.Serialize(gedcom, JsonSerializerOptions);

        File.WriteAllText(jsonFullName, jsonText);
    }

    private static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}