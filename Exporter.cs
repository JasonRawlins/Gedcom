using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Gedcom;

public class Exporter
{
    public static string[] RecordTypes = [C.FAM, C.INDI, C.OBJE, C.NOTE, C.REPO, C.SOUR, C.SUBM];
    public static string[] OutputFormats = [C.GEDC, C.JSON];

    private Options Options { get; set; }
    private Gedcom Gedcom { get; set; }

    public Exporter(Gedcom gedcom, Options options)
    {
        Gedcom = gedcom;
        Options = options ?? new Options();
    }

    public string GedcomJson() => JsonConvert.SerializeObject(Gedcom, JsonSettings.DefaultOptions);

    public string IndividualRecordJson(string xref)
    {
        var individualRecord = Gedcom.GetIndividualRecord(Options.Xref, Options.Query);
        if (individualRecord.IsEmpty)
        {
            return "";
        }

        return JsonConvert.SerializeObject(individualRecord, JsonSettings.DefaultOptions);
    }

    public string IndividualRecordsJson()
    {
        var individualRecords = Gedcom.GetIndividualRecords(Options.Query);
        return JsonConvert.SerializeObject(individualRecords, JsonSettings.DefaultOptions);
    }
    
    //public string GedcomListJson()
    //{
    //    var recordJson = "";

    //    if (Options.RecordType.Equals(C.INDI))
    //    {
    //        var individualRecordGedcomList = Gedcom.GetIndividualRecords(Options.Query).Select(ir => new GedcomListItem(ir.Xref, ir.FullName)).ToList();
    //        recordJson = JsonConvert.SerializeObject(individualRecordGedcomList);
    //    }

    //    if (Options.RecordType.Equals(C.SOUR))
    //    {
    //        var sourceRecordGedcomList = Gedcom.GetSourceRecords().Select(sr => new GedcomListItem(sr.Xref, sr.TextFromSource.Text)).ToList();
    //        recordJson = JsonConvert.SerializeObject(sourceRecordGedcomList);
    //    }

    //    return recordJson;
    //}

    //public string SourceRecordsJson()
    //{
    //    var sourceRecords = Gedcom.GetSourceRecords();
    //    return JsonConvert.SerializeObject(sourceRecords);
    //}

    public List<string> Errors
    {
        get
        {
            var argumentErrors = new List<string>();

            if (!File.Exists(Options.InputFilePath))
            {
                argumentErrors.Add($"{ErrorMessages.InputFilePathIsRequired} '{Options.InputFilePath}'");
            }

            string directoryPath = Path.GetDirectoryName(Options.OutputFilePath) ?? "";
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
                var isValidXref =  Regex.IsMatch(Options.Xref, @"@.*@");
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
        public const string InvalidRecordType = "is not a valid record type. (e.g. FAM, INDI, OBJE, NOTE, REPO, SOUR, SUBM)";
        public const string InvalidFormat = "is not a valid export format. (e.g. GEDC, JSON)";
        public const string InvalidXref = "is not a valid xref.";
    }
}

public class GedcomListItem
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
