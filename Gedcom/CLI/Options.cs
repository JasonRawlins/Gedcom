using CommandLine;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Gedcom.CLI;

public class Options
{
    public static string[] RecordTypes => [Tag.FAM, Tag.INDI, Tag.OBJE, Tag.NOTE, Tag.REPO, Tag.SOUR, Tag.SUBM, Tag.GEDC /* GEDC is not a real top-level record type. It's used when the whole gedcom is exported. */];
    public static string[] OutputFormats => [C.JSON, Tag.LIST, Tag.HTML, C.XSLX];

    private string format = C.JSON;
    [Option('f', "format", Required = false, HelpText = "Output format (json, list, html, xslx).")]
    public string Format
    {
        get => format.ToUpper();
        set => format = value;
    }

    [Option('i', "input", Required = false, HelpText = "Path of the Gedcom file.")]
    public string InputFilePath { get; set; } = "";

    [Option('o', "output", Required = false, HelpText = "Path of the output file.")]
    public string OutputFilePath { get; set; } = "";

    [Option('p', "params", Required = false, HelpText = "Path of the params file. A params file will override other all other cli arguments.")]
    public string ParamsFilePath { get; set; } = "";

    private string query = "";
    [Option('q', "query", Required = false, HelpText = "A query value to filter records.")]
    public string Query
    {
        get => query.ToUpper();
        set => query = value;
    }

    private string recordType = "";
    [Option('t', "record-type", Required = false, HelpText = "Record type to export. (gedc, fam, indi, note, obje, repo, sour, subm)")]
    public string RecordType
    {
        get => recordType.ToUpper();
        set => recordType = value;
    }    

    [Option('x', "xref", Required = false, HelpText = "Record xref. (@I123@, @R456@, @S894@, etc.")]
    public string Xref { get; set; } = "";

    public List<string> Errors
    {
        get
        {
            var argumentErrors = new List<string>();

            if (!string.IsNullOrEmpty(ParamsFilePath))
            {
                if (!File.Exists(ParamsFilePath))
                {
                    argumentErrors.Add(CliErrorMessages.GedcomNetParamsFileNotFound);
                    return argumentErrors;
                }

                var gedcomNetParams = JsonConvert.DeserializeObject<GedcomNetParams>(File.ReadAllText(ParamsFilePath));

                if (gedcomNetParams == null)
                {
                    argumentErrors.Add(CliErrorMessages.GedcomNetParamsFileIsInvalid);
                    return argumentErrors;
                }

                Format = gedcomNetParams.Format;
                InputFilePath = gedcomNetParams.Input;
                OutputFilePath = gedcomNetParams.Output;
                Query = gedcomNetParams.Query;
                RecordType = gedcomNetParams.RecordType;
                Xref = gedcomNetParams.Xref;
            }

            if (string.IsNullOrEmpty(InputFilePath))
            {
                argumentErrors.Add(CliErrorMessages.InputFilePathIsRequired);
            }

            if (!string.IsNullOrEmpty(InputFilePath) && !File.Exists(InputFilePath))
            {
                argumentErrors.Add($"{CliErrorMessages.InputFilePathIsRequired} '{InputFilePath}'");
            }

            var directoryPath = Path.GetDirectoryName(OutputFilePath) ?? "";
            if (string.IsNullOrEmpty(directoryPath))
            {
                argumentErrors.Add(CliErrorMessages.OutputFilePathIsRequired);
            }

            if (string.IsNullOrEmpty(RecordType))
            {
                argumentErrors.Add(CliErrorMessages.RecordTypeIsRequired);
            }
            
            if (!string.IsNullOrEmpty(RecordType) && !RecordTypes.Contains(RecordType.ToUpper()))
            {
                argumentErrors.Add($"'{RecordType}' {CliErrorMessages.RecordTypeIsInvalid}");
            }

            if (string.IsNullOrEmpty(Format))
            {
                argumentErrors.Add(CliErrorMessages.FormatIsRequired);
            }


            if (!string.IsNullOrEmpty(Format) && !OutputFormats.Contains(Format.ToUpper()))
            {
                argumentErrors.Add($"'{Format}' {CliErrorMessages.FormatIsInvalid}");
            }

            if (!string.IsNullOrEmpty(Xref))
            {
                var isValidXref = Regex.IsMatch(Xref, @"@.*@");
                if (!isValidXref)
                {
                    // It looks like Ancestry is the one that prepends a letter to the xrefs based on 
                    // type, like "I" for INDI xrefs ("@I234@"). This is not part of the standard.
                    // See comment below on xref_ID for more details.
                    argumentErrors.Add($"{Xref} {CliErrorMessages.XrefIsInvalid}");
                }
            }

            return argumentErrors;
        }
    }
}