using CommandLine;
using System.Text.RegularExpressions;
using static Gedcom.Ancestry;

namespace Gedcom;

public class Options
{
    [Option('i', "input", Required = true, HelpText = "Input file path.")]
    public string InputFilePath { get; set; } = "";

    [Option('o', "output", Required = true, HelpText = "Output file path.")]
    public string OutputFilePath { get; set; } = "";

    [Option('f', "format", Required = false, HelpText = "Sets the output format (e.g. json).")]
    public string Format { get; set; } = "JSON";

    [Option('x', "xref", Required = false, HelpText = "Record xref. (e.g. @I123@, @R456@, @S894, etc.")]
    public string Xref { get; set; } = "";

    [Option('t', "record-type", Required = false, HelpText = "Record type to export. (e.g. GEDC, FAM, INDI, OBJE, NOTE, REPO, SOUR, SUBM)")]
    public string RecordType { get; set; } = C.GEDC;

    [Option('q', "query", Required = false, HelpText = "The query that is used to filter records by query value")]
    public string Query { get; set; } = "";

    [Option('l', "list", Required = false, HelpText = "Lists all records of the record type.")]
    public bool List { get; set; } = false;

    public List<string> ArgumentErrors
    {
        get
        {
            var argumentErrors = new List<string>();

            if (string.IsNullOrEmpty(InputFilePath) || !File.Exists(InputFilePath))
            {
                argumentErrors.Add($"Could not find the file '{InputFilePath}'");
            }

            var isValidXref = Regex.IsMatch(Xref, @"@.*@");
            var hasRecordType = !string.IsNullOrEmpty(RecordType);

            if (isValidXref && !hasRecordType)
            {
                // It looks like Ancestry is the one that prepends a letter to the xrefs based on 
                // type, like "I" for INDI xrefs ("@I234@"). This is not part of the standard.
                // See comment below on xref_ID for more details.
                argumentErrors.Add("If an xref is specified, then a record type must also be specified. (e.g. FAM, INDI, OBJE, NOTE, REPO, SOUR, SUBM)");
            }

            

            if (!string.IsNullOrEmpty(Xref))
            {
                if (!isValidXref)
                {
                    argumentErrors.Add("Invalid xref. It should be an @ sign, a letter, a number, and an '@' sign. (e.g. @I123@, @S456@, @R843@, etc.)");
                }
            }

            if (List && !string.IsNullOrEmpty(RecordType))
            {
                argumentErrors.Add("If -list is specified, -type must also be specified.");
            }

            var acceptedFormats = new string[] { C.GEDC, C.JSON };
            if (!acceptedFormats.Contains(Format.ToUpper())) 
            {
                argumentErrors.Add($"{Format} is not a valid format.");
            }


            return argumentErrors;
        }
    }
}

/*
The Gedcom Standard 5.1.1. p. 17.

xref_ID:=
    (See pointer, page 16)
    The xref_ID is formed by any arbitrary combination of characters from the pointer_char set.
    The first character must be an alpha or a digit. The xref_ID is not retained in the receiving
    system, and it may therefore be formed from any convenient combination of identifiers from the
    sending system. No meaning is attributed by the receiver to any part of the xref_ID, other than its
    unique association with the associated record. The use of the colon (:) character is also reserved.
*/