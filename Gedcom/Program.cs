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
    }

    private static void WriteIndividualRecords(IGedcomWriter gedcomWriter, Options options)
    {
        if (options.Format.Equals(Constants.Excel) || options.Format.Equals(Constants.HTML))
        {
            // HACK: Exporting as Excel does not cleanly map to IGedcomWriter. 
            // This is for temporary demonstration purposes.
            File.WriteAllBytes(options.OutputFilePath, gedcomWriter.GetAsByteArray());
        }
        else
        {
            var gedcomText = string.IsNullOrEmpty(options.Xref)
                ? gedcomWriter.GetIndividuals(options.Query)
                : gedcomWriter.GetIndividual(options.Xref);

            File.WriteAllText(options.OutputFilePath, gedcomText);
        }
    }
}