namespace Gedcom.CLI;

public static class CliErrorMessages
{
    public const string FormatIsInvalid = "is not a valid export format. (html, json, list, xlsx)";
    public const string FormatIsRequired = "Format is required.";
    public const string GedcomNetParamsFileIsInvalid = "The params file is invalid.";
    public const string GedcomNetParamsFileNotFound = "Could not find the params file.";
    public const string InputFilePathIsRequired = "Input file path is required.";
    public const string InputFilePathNotFound = "Could not find the input file:";
    public const string OutputFilePathIsRequired = "Output file path is required.";
    public const string RecordTypeIsInvalid = "is not a valid record type. (fam, indi, note, obje, repo, sour, subm)";
    public const string RecordTypeIsRequired = "Record type is required.";
    public const string XrefIsInvalid = "is not a valid xref.";
}