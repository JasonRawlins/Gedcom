using CommandLine;
using Gedcom;
using Gedcom.CLI;
using Gedcom.GedcomWriters;

public class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);
    }

    private static async Task<Gedcom.Core.Gedcom> CreateGedcomAsync(string gedFullName)
    {
        var gedFileLines = await File.ReadAllLinesAsync(gedFullName);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        return new Gedcom.Core.Gedcom(gedcomLines);
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
        
        var gedcom = CreateGedcomAsync(options.InputFilePath).Result;
        var gedcomWriter = GedcomWriter.Create(gedcom, options.Format);

        if (options.RecordType.Equals(Tag.Individual))
        {
            var gedcomText = string.IsNullOrEmpty(options.Xref)
                ? gedcomWriter.GetIndividuals()
                : gedcomWriter.GetIndividual(options.Xref);

            File.WriteAllText(options.OutputFilePath, gedcomText);
        }
    }
}