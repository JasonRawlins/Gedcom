using CommandLine;
using Gedcom;

public class Program
{
    private static Options Options { get; set; } = new Options();

    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);

        Console.ReadLine();
    }

    static void RunOptions(Options options)
    {
        Options = options;

        if (Options.ArgumentErrors.Count > 0)
        {
            Options.ArgumentErrors.ForEach(ae => Console.WriteLine(ae));
            return;
        }

        var gedcom = CreateGedcom(Options.InputFilePath);
        var exporter = new Exporter(gedcom, Options);

        if (Options.RecordType.ToUpper().Equals(C.GEDC))
        {
            WriteAllText(exporter.GedcomJson());
            return;
        }

        //if (Options.List)
        //{
        //    WriteAllText(exporter.GedcomListJson());
        //    return;
        //}

        if (Options.RecordType.ToUpper().Equals(C.INDI))
        {
            WriteAllText(exporter.IndividualRecordsJson());
            return;
        }

        if (Options.RecordType.ToUpper().Equals(C.SOUR))
        {
            WriteAllText(exporter.SourceRecordsJson());
            return;
        }
    }

    private static Gedcom.Gedcom CreateGedcom(string gedFullName)
    {
        var gedFileLines = File.ReadAllLines(gedFullName);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        return new Gedcom.Gedcom(gedcomLines);
    }
    
    private static void WriteAllText(string recordJson)
    {
        Console.WriteLine(recordJson);
        File.WriteAllText(Options.OutputFilePath, recordJson);
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
