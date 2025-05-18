using CommandLine;
using Gedcom;
using Gedcom.CLI;
using Gedcom.RecordStructures;
using System.Text;

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
            WriteJson(exporter.GedcomJson());
            return;
        }

        if (options.RecordType.Equals(C.INDI))
        {
            if (options.Format.Equals(C.JSON))
            {
                WriteJson(exporter.IndividualRecordsJson());
            }
            else if (options.Format.Equals(C.LIST))
            {
                //WriteList(exporter.IndividualListItems());
            }
            else if (options.Format.Equals(C.HTML))
            {
                WriteHtml(exporter.IndividualsHtml());
            }

            return;
        }

        // Find a record by query.
        if (!string.IsNullOrEmpty(options.Xref))
        {
            if (options.RecordType.Equals(C.INDI))
            {
                WriteJson(exporter.IndividualRecordJson());
            }
        }

        //if (Options.RecordType.Equals(C.SOUR))
        //{
        //    WriteAllText(exporter.SourceRecordsJson());
        //    return;
        //}

        void WriteJson(string recordJson)
        {
            Console.WriteLine(recordJson);
            File.WriteAllText(options.OutputFilePath, recordJson);
        }

        void WriteList(List<IndividualListItem> individualListItems)
        {
            var individualsList = individualListItems.Select(ili => ili.ToString());
            foreach (var individualListItem in individualsList)
            {
                Console.WriteLine(individualListItem);
            }

            File.WriteAllLines(options.OutputFilePath, individualsList);
        }

        void WriteHtml(string html)
        {        
            File.WriteAllText(options.OutputFilePath, html);
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
