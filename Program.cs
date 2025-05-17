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
        var gedcom = CreateGedcom(options.InputFilePath);
        var exporter = new Exporter(gedcom, options);

        if (exporter.Errors.Count > 0)
        {
            exporter.Errors.ForEach(ae => Console.WriteLine(ae));
            return;
        }

        if (options.RecordType.Equals(C.GEDC))
        {
            WriteAllText(exporter.GedcomJson());
            return;
        }

        if (options.RecordType.Equals(C.INDI))
        {
            WriteAllText(exporter.IndividualRecordsJson());
            return;
        }

        // Find a record by query.
        if (!string.IsNullOrEmpty(options.Xref))
        {
            if (options.RecordType.Equals(C.INDI))
            {
                WriteAllText(exporter.IndividualRecordJson(options.Xref));
            }
        }

        //if (Options.RecordType.Equals(C.SOUR))
        //{
        //    WriteAllText(exporter.SourceRecordsJson());
        //    return;
        //}

        void WriteAllText(string recordJson)
        {
            Console.WriteLine(recordJson);
            File.WriteAllText(options.OutputFilePath, recordJson);
        }
    }

    static void HandleParseError(IEnumerable<Error> errors)
    {
        Console.WriteLine("Parsing failed for command line arguments");
        foreach (Error error in errors)
        {
            Console.WriteLine(error);
        }
    }

    private static Gedcom.Gedcom CreateGedcom(string gedFullName)
    {
        var gedFileLines = File.ReadAllLines(gedFullName);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        return new Gedcom.Gedcom(gedcomLines);
    }
}
