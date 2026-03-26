using CommandLine;
using Gedcom.CLI;
using Gedcom.GedcomWriters;

namespace Gedcom;

public class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<CliOptions>(args)
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

    private static void RunOptions(CliOptions options)
    {
        if (options.Errors.Count > 0)
        {
            options.Errors.ForEach(Console.WriteLine);
            return;
        }

        WriteRecords(options);
    }

    private static void WriteRecords(CliOptions options)
    {
        var gedcomDocument = CreateGedcomDocument(options.InputFilePath);
        var gedcomWriter = GedcomWriter.Create(gedcomDocument, options.Format);

        if (options.RecordType.Equals(Tag.Family, StringComparison.OrdinalIgnoreCase))
        {
            var familiesBytes = gedcomWriter.GetFamilies(options.Xref);
            File.WriteAllBytes(options.OutputFilePath, familiesBytes);
        }

        if (options.RecordType.Equals(Tag.Individual, StringComparison.OrdinalIgnoreCase))
        {
            var individualsBytes = gedcomWriter.GetIndividuals(options.Xref);
            File.WriteAllBytes(options.OutputFilePath, individualsBytes);
        }

        if (options.RecordType.Equals(Tag.Repository, StringComparison.OrdinalIgnoreCase))
        {
            var repositoriesBytes = gedcomWriter.GetRepositories(options.Xref);
            File.WriteAllBytes(options.OutputFilePath, repositoriesBytes);
        }

        if (options.RecordType.Equals(Tag.Source, StringComparison.OrdinalIgnoreCase))
        {
            var sourcesBytes = gedcomWriter.GetSources(options.Xref);
            File.WriteAllBytes(options.OutputFilePath, sourcesBytes);
        }
    }
}