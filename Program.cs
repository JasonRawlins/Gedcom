using CommandLine;
using Gedcom;

public class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);

        Console.ReadLine();
    }

    static void RunOptions(Options options)
    {
        if (options.ArgumentErrors.Count > 0)
        {
            options.ArgumentErrors.ForEach(ae => Console.WriteLine(ae));
            return;
        }

        var gedcom = CreateGedcom(options.InputFilePath);
        var exporter = new Exporter(gedcom, options);

        if (options.RecordType.ToUpper().Equals(C.GEDC))
        {
            exporter.ExportGedJson();
            return;
        }

        if (options.List)
        {
            exporter.ExportGedcomList();
            return;
        }

        if (options.RecordType.ToUpper().Equals(C.INDI))
        {
            exporter.ExportIndividualRecords();
            return;
        }

        if (options.RecordType.ToUpper().Equals(C.SOUR))
        {
            exporter.ExportSourceRecords();
            return;
        }
    }

    private static Gedcom.Gedcom CreateGedcom(string gedFullName)
    {
        var gedFileLines = File.ReadAllLines(gedFullName);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        return new Gedcom.Gedcom(gedcomLines);
    }

    static void HandleParseError(IEnumerable<Error> errors)
    {
        Console.WriteLine("Parsing failed for command line arguments");
        foreach (Error error in errors)
        {
            Console.WriteLine(error);
        }
    }
}
