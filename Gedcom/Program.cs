using CommandLine;
using Gedcom;
using Gedcom.CLI;
using System.Diagnostics;

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
        var gedcom = CreateGedcom(options.GedPath);
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
            else if (options.Format.Equals(C.HTML))
            {
                WriteHtml(exporter.IndividualsHtml());
            }
            else if (options.Format.Equals(C.XSLX))
            {
                File.WriteAllBytes(options.OutputFilePath, exporter.IndividualsExcel());
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

        void WriteJson(string json)
        {
            File.WriteAllText(options.OutputFilePath, json);
        }

        void WriteHtml(string html)
        {
            File.WriteAllText(options.OutputFilePath, html);

            Process.Start(new ProcessStartInfo
            {
                FileName = options.OutputFilePath,
                UseShellExecute = true
            });
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
