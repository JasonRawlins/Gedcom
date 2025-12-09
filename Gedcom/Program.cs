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

        if (options.RecordType.Equals(Tag.GEDC))
        {
            WriteJson(exporter.GedcomJson(), options.OutputFilePath);
            return;
        }

        if (options.RecordType.Equals(Tag.INDI))
        {
            if (options.Format.Equals(C.JSON))
            {
                WriteJson(exporter.IndividualRecordsJson(), options.OutputFilePath);
            }
            else if (options.Format.Equals(Tag.HTML))
            {
                WriteHtml(exporter.IndividualsHtml(), options.OutputFilePath);
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
            if (options.RecordType.Equals(Tag.INDI))
            {
                WriteJson(exporter.IndividualRecordJson(), options.OutputFilePath);
            }
        }

       
    }

    static void WriteJson(string json, string outputFilePath)
    {
        File.WriteAllText(outputFilePath, json);
    }

    static void WriteHtml(string html, string outputFilePath)
    {
        File.WriteAllText(outputFilePath, html);

        Process.Start(new ProcessStartInfo
        {
            FileName = outputFilePath,
            UseShellExecute = true
        });
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
