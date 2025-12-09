using CommandLine;

namespace Gedcom.CLI;

public class Options
{
    private string format = C.JSON;
    [Option('f', "format", Required = false, HelpText = "Output format (json, ged, list).")]
    public string Format
    {
        get => format.ToUpper();
        set => format = value;
    }

    [Option('g', "ged", Required = true, HelpText = "Path to .ged file.")]
    public string GedPath { get; set; } = "";

    [Option('o', "output", Required = true, HelpText = "Output file path.")]
    public string OutputFilePath { get; set; } = "";

    private string query = "";
    [Option('q', "query", Required = false, HelpText = "Filters records by a query value")]
    public string Query
    {
        get => query.ToUpper();
        set => query = value;
    }

    private string recordType = "";
    [Option('t', "record-type", Required = true, HelpText = "Record type to export. (GEDC, FAM, INDI, OBJE, NOTE, REPO, SOUR, SUBM)")]
    public string RecordType
    {
        get => recordType.ToUpper();
        set => recordType = value;
    }    

    [Option('x', "xref", Required = false, HelpText = "Record xref. (@I123@, @R456@, @S894, etc.")]
    public string Xref { get; set; } = "";
}

/*
The Gedcom Standard 5.5.1. p. 17.

xref_ID:=
    (See pointer, page 16)
    The xref_ID is formed by any arbitrary combination of characters from the pointer_char set.
    The first character must be an alpha or a digit. The xref_ID is not retained in the receiving
    system, and it may therefore be formed from any convenient combination of identifiers from the
    sending system. No meaning is attributed by the receiver to any part of the xref_ID, other than its
    unique association with the associated record. The use of the colon (:) character is also reserved.
*/