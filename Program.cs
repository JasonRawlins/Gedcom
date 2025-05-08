using CommandLine;
using Gedcom;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);
    }

    static void RunOptions(Options options)
    {
        if (!File.Exists(options.InputFilePath))
        {
            Console.WriteLine($"Could not find the file '{options.InputFilePath}'");
            return;
        }

        var gedcom = CreateGedcom(options.InputFilePath);

        switch (options.Format.ToUpper())
        {
            case "JSON":
                ExportGedAsJson(gedcom, options.OutputFilePath);
                break;
            default:
                Console.WriteLine($"{options.Format} is not a valid format.");
                break;
        }
    }

    private static Gedcom.Gedcom CreateGedcom(string gedFullName)
    {
        var gedFileLines = File.ReadAllLines(gedFullName);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        return new Gedcom.Gedcom(gedcomLines);
    }

    private static void ExportGedAsJson(Gedcom.Gedcom gedcom, string outputFilePath)
    {
        var jsonText = JsonSerializer.Serialize(gedcom, JsonSerializerOptions);
        File.WriteAllText(outputFilePath, jsonText);

    }

    static void HandleParseError(IEnumerable<Error> errors)
    {
        Console.WriteLine("Parsing failed for command line arguments");
        foreach (Error error in errors)
        {
            Console.WriteLine(error);
        }
        Console.ReadLine();
    }

    private static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}