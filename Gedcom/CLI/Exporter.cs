using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Gedcom.CLI;

public class Exporter(Gedcom gedcom, Options options)
{
    public static string[] RecordTypes => [C.FAM, C.INDI, C.OBJE, C.NOTE, C.REPO, C.SOUR, C.SUBM, C.GEDC /* GEDC is not a real top-level record type. It's used when the whole gedcom is exported. */];
    public static string[] OutputFormats => [C.JSON, C.LIST, C.HTML, C.XSLX];

    public Options Options { get; set; } = options ?? new Options();
    public Gedcom Gedcom { get; set; } = gedcom;

    public Exporter(Gedcom gedcom) : this(gedcom, new Options())
    {
    }

    public string GedcomJson() => JsonConvert.SerializeObject(Gedcom, JsonSettings.DefaultOptions);

    // Family (FAM)
    public string FamilyRecordJson() => GetRecordJson(Gedcom.GetFamilyRecord(Options.Xref, Options.Query));
    public string FamilyRecordsJson() => GetRecordsJson(Gedcom.GetFamilyRecords(Options.Query));

    // Individual (INDI)
    public string IndividualRecordJson() => GetRecordJson(Gedcom.GetIndividualRecord(Options.Xref, Options.Query));
    public string IndividualRecordsJson() => GetRecordsJson(Gedcom.GetIndividualRecords(Options.Query));
    public string IndividualsHtml()
    {
        var individualListItems = Gedcom.GetIndividualRecords().Select(ir => new IndividualListItem(ir)).ToList();

        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.IndividualsHtmlTemplate);
        var individualLis = HtmlWriter.CreateIndividualLis(individualListItems, Gedcom.Header.Source.Tree.AutomatedRecordId);
        var finalHtml = htmlTemplate.Replace("{{INDIVIDUAL_LIST_ITEMS}}", string.Join(Environment.NewLine, individualLis));

        return finalHtml;
    }

    public byte[] IndividualsExcel()
    {
        var individualListItems = Gedcom.GetIndividualRecords().Select(ir => new IndividualListItem(ir)).ToList();
        var excelWriter = new ExcelWriter(Gedcom.Header.Source.Tree);
        return excelWriter.GetIndividuals(individualListItems);
    }

    // Repository (REPO)
    public string RepositoryRecordJson() => GetRecordJson(Gedcom.GetRepositoryRecord(Options.Xref));
    public string RepositoryRecordsJson() => GetRecordsJson(Gedcom.GetRepositoryRecords(Options.Query));

    // Source (SOUR)
    public string SourceRecordJson() => GetRecordJson(Gedcom.GetSourceRecord(Options.Xref));
    public string SourceRecordsJson() => GetRecordsJson(Gedcom.GetSourceRecords(Options.Query));

    private static string GetRecordJson(RecordStructureBase recordStructure)
    {
        if (recordStructure.IsEmpty) return "";
        return JsonConvert.SerializeObject(recordStructure, JsonSettings.DefaultOptions);
    }

    private static string GetRecordsJson(IEnumerable<RecordStructureBase> recordStructures)
    {
        if (!recordStructures.Any()) return "";
        return JsonConvert.SerializeObject(recordStructures, JsonSettings.DefaultOptions);
    }

    public string GetCliCommand()
    {
        return $"gedcom -i {Options.GedPath} -o {Options.OutputFilePath} -t {Options.RecordType} -f {Options.Format} -x {Options.Xref}";
    }

    public List<string> Errors
    {
        get
        {
            var argumentErrors = new List<string>();

            if (!File.Exists(Options.GedPath))
            {
                argumentErrors.Add($"{ErrorMessages.InputFilePathIsRequired} '{Options.GedPath}'");
            }

            string directoryPath = System.IO.Path.GetDirectoryName(Options.OutputFilePath) ?? "";
            if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            {
                argumentErrors.Add(ErrorMessages.OutputFilePathIsRequired);
            }

            if (!RecordTypes.Contains(Options.RecordType))
            {
                argumentErrors.Add($"{Options.RecordType} {ErrorMessages.InvalidRecordType}");
            }

            if (!OutputFormats.Contains(Options.Format))
            {
                argumentErrors.Add($"{Options.Format} {ErrorMessages.InvalidFormat}");
            }

            if (!string.IsNullOrEmpty(Options.Xref))
            {
                var isValidXref = Regex.IsMatch(Options.Xref, @"@.*@");
                if (!isValidXref)
                {
                    // It looks like Ancestry is the one that prepends a letter to the xrefs based on 
                    // type, like "I" for INDI xrefs ("@I234@"). This is not part of the standard.
                    // See comment below on xref_ID for more details.
                    argumentErrors.Add($"{Options.Xref} {ErrorMessages.InvalidXref}");
                }
            }

            return argumentErrors;
        }
    }

    public class ErrorMessages
    {
        public const string InputFilePathIsRequired = "Could not find the input file:";
        public const string OutputFilePathIsRequired = "The output file path must refer to an existing directory.";
        public const string InvalidRecordType = "is not a valid record type. (FAM, INDI, OBJE, NOTE, REPO, SOUR, SUBM)";
        public const string InvalidFormat = "is not a valid export format. (JSON, LIST)";
        public const string InvalidXref = "is not a valid xref.";
    }
}