using CommandLine;

namespace Gedcom;

public class Options
{
    [Option('i', "input", Required = true, HelpText = "Input file path.")]
    public string InputFilePath { get; set; } = "";

    [Option('o', "output", Required = true, HelpText = "Output file path.")]
    public string OutputFilePath { get; set; } = "";

    [Option('f', "format", Required = true, HelpText = "Set output format to json.")]
    public string Format { get; set; } = "";
}