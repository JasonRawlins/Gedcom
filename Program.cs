using CommandLine;
using Gedcom;
using Gedcom.RecordStructures;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        if (options.RecordType.ToUpper().Equals(C.GEDC))
        {
            ExportGedJson(gedcom, options);
            return;
        }

        if (options.List)
        {
            ExportGedcomList(gedcom, options);
            return;
        }

        if (options.RecordType.ToUpper().Equals(C.INDI))
        {
            ExportIndividualRecords(gedcom, options);
            return;
        }

        if (options.RecordType.ToUpper().Equals(C.SOUR))
        {
            ExportSourceRecords(gedcom, options);
            return;
        }
    }

    private static Gedcom.Gedcom CreateGedcom(string gedFullName)
    {
        var gedFileLines = File.ReadAllLines(gedFullName);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        return new Gedcom.Gedcom(gedcomLines);
    }

    private static void ExportGedJson(Gedcom.Gedcom gedcom, Options options)
    {
        if (options.Format.ToUpper().Equals(C.JSON))
        {
            var gedcomJson = JsonSerializer.Serialize(gedcom, JsonSerializerOptions);
            File.WriteAllText(options.OutputFilePath, gedcomJson);
        }
    }

    private static void ExportIndividualRecords(Gedcom.Gedcom gedcom, Options options)
    {
        if (string.IsNullOrEmpty(options.Xref))
        {
            // If an xref isn't defined, export all individual records.
            if (options.Format.ToUpper().Equals(C.JSON))
            {
                var individualRecords = new List<IndividualRecord>();

                if (string.IsNullOrEmpty(options.Query))
                {
                    individualRecords = gedcom.GetIndividualRecords();
                }
                else
                {
                    individualRecords = gedcom.GetIndividualRecords(options.Query);
                }

                var individualRecordsJson = JsonSerializer.Serialize(individualRecords, JsonSerializerOptions);
                Console.Write(individualRecordsJson);
                File.WriteAllText(options.OutputFilePath, individualRecordsJson);
            }
        }
        else
        {
            // If an xref is defined, export that individualRecord.
            var individualRecord = gedcom.GetIndividualRecord(options.Xref);
            var individualRecordJson = JsonSerializer.Serialize(individualRecord, JsonSerializerOptions);
            File.WriteAllText(options.OutputFilePath, individualRecordJson);
        }
    }

    private static void ExportGedcomList(Gedcom.Gedcom gedcom, Options options)
    {
        var recordJson = "";
        if (options.RecordType.Equals(C.INDI))
        {
            var individualRecordGedcomList = gedcom.GetIndividualRecords().Select(ir => new GedcomListItem(ir.Xref, ir.FullName)).ToList();
            recordJson = JsonSerializer.Serialize(individualRecordGedcomList, JsonSerializerOptions);
        }

        if (options.RecordType.Equals(C.SOUR))
        {
            var sourceRecordGedcomList = gedcom.GetSourceRecords().Select(sr => new GedcomListItem(sr.Xref, sr.TextFromSource.Text)).ToList();
            recordJson = JsonSerializer.Serialize(sourceRecordGedcomList, JsonSerializerOptions); 
        }

        Console.WriteLine(recordJson);
        File.WriteAllText(options.OutputFilePath, recordJson);
    }

    internal class GedcomListItem
    {
        public GedcomListItem(string xref, string value)
        {
            Xref = xref;
            Value = value;
        }

        public string Xref { get; set; } = "";
        public string Value { get; set; } = "";

        public override string ToString() => $"{Value} ({Xref})";
    }

    private static void ExportSourceRecords(Gedcom.Gedcom gedcom, Options options)
    {
        var sourceRecordJson = JsonSerializer.Serialize(gedcom.GetSourceRecords(), JsonSerializerOptions);
        File.WriteAllText(options.OutputFilePath, sourceRecordJson);
    }

    private static void ExportIndividualRecord(IndividualRecord individualRecord, Options options)
    {
        var individualRecordJson = JsonSerializer.Serialize(individualRecord, JsonSerializerOptions);
        File.WriteAllText(options.OutputFilePath, individualRecordJson);
    }

    static void HandleParseError(IEnumerable<Error> errors)
    {
        Console.WriteLine("Parsing failed for command line arguments");
        foreach (Error error in errors)
        {
            Console.WriteLine(error);
        }
        Console.ReadLine();
    }

    private static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
    