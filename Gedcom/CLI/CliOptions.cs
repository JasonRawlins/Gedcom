using System.Text.Json;
using CommandLine;
using Gedcom.DTOs;

namespace Gedcom.CLI;

public class CliOptions
{
    public static string[] RecordTypes => [Tag.Family, Tag.Individual, Tag.Object, Tag.Note, Tag.Repository, Tag.Source, Tag.Submitter, Tag.Gedcom /* GEDC is not a real top-level record type. It's used when the whole gedcom is exported. */];
    public static string[] OutputFormats => [Constants.Excel, Constants.Html, Constants.Json, Constants.Text];

    private string format = Constants.Json;
    [Option('f', "format", Required = false, HelpText = "Output format (Excel, html, json, text).")]
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

    // The following record types are not supported yet: gedc, note, obje, subm.
    private string recordType = "";
    [Option('r', "record-type", Required = false, HelpText = "Record type to export. (fam, indi, repo, sour)")]
    public string RecordType
    {
        get => recordType.ToUpper();
        set => recordType = value;
    }    

    [Option('x', "xref", Required = false, HelpText = "Record xref. (@I123@, @R456@, @S894@, etc.")]
    public string Xref { get; set; } = "";

    public void ApplyParamsFile()
    {
        // If a params file is specified, it overwrites all other cli arguments.
        if (!string.IsNullOrEmpty(ParamsFilePath))
        {
            if (!File.Exists(ParamsFilePath))
            {
                throw new FileNotFoundException($"Could not file the cli params file: {ParamsFilePath}");
            }

            var gedcomNetCliParamsText = File.ReadAllText(ParamsFilePath);
            var gedcomNetCliParams = JsonSerializer.Deserialize<GedcomNetCliParams>(gedcomNetCliParamsText, GedcomDto.SerializationOptions);

            if (gedcomNetCliParams == null)
            {
                throw new InvalidOperationException($"{CliErrorMessages.ParamsFileDeserializationFailed} '{ParamsFilePath}'");
            }

            if (!string.IsNullOrEmpty(InputFilePath) && !File.Exists(InputFilePath))
            {
                throw new FileNotFoundException($"{CliErrorMessages.InputFileNotFound} '{InputFilePath}'");
            }

            Format = gedcomNetCliParams.Format;
            InputFilePath = gedcomNetCliParams.Input;
            OutputFilePath = gedcomNetCliParams.Output;
            RecordType = gedcomNetCliParams.RecordType;
            Xref = gedcomNetCliParams.Xref;
        }
    }

    public List<string> Errors
    {
        get
        {
            var argumentErrors = new List<string>();

            if (string.IsNullOrEmpty(Format))
            {
                argumentErrors.Add(CliErrorMessages.FormatIsRequired);
            }

            if (!string.IsNullOrEmpty(Format) && !OutputFormats.Select(of => of.ToUpper()).Contains(Format.ToUpper()))
            {
                argumentErrors.Add($"'{Format}' {CliErrorMessages.FormatIsInvalid}");
            }

            if (string.IsNullOrEmpty(InputFilePath))
            {
                argumentErrors.Add(CliErrorMessages.InputFilePathIsRequired);
            }

            if (string.IsNullOrEmpty(OutputFilePath))
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

            if (!string.IsNullOrEmpty(Xref))
            {
                if (!Constants.XrefRegex().IsMatch(Xref))
                {
                    argumentErrors.Add($"'{Xref}' {CliErrorMessages.XrefIsInvalid}");
                }
            }

            return argumentErrors;
        }
    }
}