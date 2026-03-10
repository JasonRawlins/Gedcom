using CommandLine;
using Gedcom.CLI;
using Gedcom.GedcomWriters;

namespace Gedcom;

public class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);
    }

    private static GedcomDocument CreateGedcomDocument(string gedFullName)
    {
        var gedFileLines = File.ReadAllLines(gedFullName);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();

        return new GedcomDocument(gedcomLines);
    }

    private static void HandleParseError(IEnumerable<Error> errors)
    {
        foreach (Error error in errors)
        {
            Console.WriteLine(error);
        }
    }

    private static void RunOptions(Options options)
    {
        if (options.Errors.Count > 0)
        {
            options.Errors.ForEach(Console.WriteLine);
            return;
        }

        WriteRecords(options);
    }

    private static void WriteRecords(Options options)
    {
        var gedcomDocument = CreateGedcomDocument(options.InputFilePath);
        var gedcomWriter = GedcomWriter.Create(gedcomDocument, options.Format);

        if (options.RecordType.Equals(Tag.Individual))
        {
            WriteIndividualRecords(gedcomWriter, options);
        }

        if (options.RecordType.Equals(Tag.Family))
        {
            WriteFamilyRecords(gedcomWriter, options);
        }
    }

    private static void WriteIndividualRecords(IGedcomWriter gedcomWriter, Options options)
    {
        var individualBytes = gedcomWriter.GetIndividuals(options.Xref);

        File.WriteAllBytes(options.OutputFilePath, individualBytes);
    }

    private static void WriteFamilyRecords(IGedcomWriter gedcomWriter, Options options)
    {
        var familyBytes = gedcomWriter.GetFamilies(options.Xref);

        File.WriteAllBytes(options.OutputFilePath, familyBytes);
    }
}