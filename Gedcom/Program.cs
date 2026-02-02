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

    private static GedcomDocument CreateGedcom(string gedFullName)
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
        
        var gedcom = CreateGedcom(options.InputFilePath);
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