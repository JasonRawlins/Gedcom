using CommandLine;
using Gedcom;
using Gedcom.CLI;
using System.Diagnostics;

public class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);
    }

    private static void RunOptions(Options options)
    {
        if (options.Errors.Count > 0)
        {
            options.Errors.ForEach(Console.WriteLine);           
            return;
        }

        var gedcom = CreateGedcom(options.InputFilePath);
        var exporter = new Exporter(gedcom);

        if (options.RecordType.Equals(Tag.GEDC))
        {
            WriteJson(exporter.GetGedcomJson(), options.OutputFilePath);
            return;
        }

        if (options.RecordType.Equals(Tag.INDI))
        {
            if (options.Format.Equals(C.JSON))
            {
                if (string.IsNullOrEmpty(options.Xref))
                {
                    WriteJson(exporter.GetIndividualsJson(options.Query), options.OutputFilePath);
                }
                else
                {
                    WriteJson(exporter.GetIndividualJson(options.Xref), options.OutputFilePath);
                }
            }
            else if (options.Format.Equals(Tag.HTML))
            {
                WriteHtml(exporter.GetIndividualsHtml(), options.OutputFilePath);
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
                if (string.IsNullOrEmpty(options.Xref))
                {
                    WriteJson(exporter.GetIndividualsJson(options.Xref), options.OutputFilePath);
                }
                else
                {
                    WriteJson(exporter.GetIndividualJson(options.Xref), options.OutputFilePath);
                }
            }
        }
    }

    private static void HandleParseError(IEnumerable<Error> errors)
    {
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

    private static void WriteJson(string json, string outputFilePath)
    {
        File.WriteAllText(outputFilePath, json);
    }

    private static void WriteHtml(string html, string outputFilePath)
    {
        File.WriteAllText(outputFilePath, html);

        Process.Start(new ProcessStartInfo
        {
            FileName = outputFilePath,
            UseShellExecute = true
        });
    }
}